using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;
using ReconciliationModel = GoodNeighborHouse.TimeCard.Web.Models.Reconciliation;
using ReconciliationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class ReconciliationController : Controller
    {

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<ReconciliationEntity, ReconciliationModel> _reconciliationConverter;
        private readonly IMapper<ReconciliationModel, ReconciliationEntity> _reconciliationMapper;
        private readonly IConverter<PunchEntity, PunchModel> _punchConverter;
        private readonly IMapper<PunchModel, PunchEntity> _punchMapper;

        public ReconciliationController(IUnitOfWorkFactory unitOfWorkFactory,
        IConverter<ReconciliationEntity, ReconciliationModel> reconciliationConverter,
        IMapper<ReconciliationModel, ReconciliationEntity> reconciliationMapper,
        IConverter<PunchEntity, PunchModel> punchConverter,
        IMapper<PunchModel, PunchEntity> punchMapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _reconciliationConverter = reconciliationConverter;
            _reconciliationMapper = reconciliationMapper;
            _punchConverter = punchConverter;
            _punchMapper = punchMapper;
        }


        // GET: Reconciliation
        public ActionResult Index()
        {
            return View();
        }

        // POST: Reconciliation/Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid volunteerId,
            DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            using (var PunchUnitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var punches = (await PunchUnitOfWork.GetRepository<IPunchRepository>()
                .GetAllForVolunteerInPeriod(volunteerId, start, end)
                .ToImmutableArrayAsync(cancellationToken))
                .Select(_punchConverter.Convert)
                .ToImmutableArray();

                return View(@"Edit", punches);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(Guid volunteerId,
            DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            IEnumerable<PunchModel> punches = null;
            using (var UnitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {

                punches = (await UnitOfWork.GetRepository<IPunchRepository>()
                    .GetAllForVolunteerInPeriod(volunteerId, start, end).ToImmutableArrayAsync()).
                    Select(p=>_punchConverter.Convert(p)).ToImmutableArray();
                var reconciliationRepository = UnitOfWork.GetRepository<IReconciliationRepository>();
                reconciliationRepository.ClearAllForVolunteerInPeriod(volunteerId, start, end);
                ReconciliationEntity reconciliation = new ReconciliationEntity();
                var newRecs = new LinkedList<ReconciliationEntity>();
                foreach (var punch in punches.Where(p => !p.IsDeleted))
                {
                    if (punch.IsClockIn) {
                        reconciliation = new ReconciliationEntity
                        {
                            PunchInId = punch.Id,
                            ApprovedBy = 0
                        };
                    } 
                    else {
                        reconciliation.ApprovedOn = DateTime.Now;
                        reconciliation.PunchOutId = punch.Id;
                        reconciliation.Difference = (long)(reconciliation.PunchOut.PunchTime - reconciliation.PunchIn.PunchTime).TotalMilliseconds;
                        newRecs.AddFirst(reconciliation);
                    }

                }
              await reconciliationRepository.AddAllAsync(newRecs);
                
            }
            if (punches is null) {
                return NotFound(); 
            }
            return View(@"Edit", punches);
        }

    }
}