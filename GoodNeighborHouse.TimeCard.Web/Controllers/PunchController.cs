using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Http;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using System.Threading;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class PunchController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<PunchEntity, PunchModel> _converter;
        private readonly IMapper<PunchModel, PunchEntity> _mapper;

        public PunchController(IUnitOfWorkFactory unitOfWorkFactory, IConverter<PunchEntity, PunchModel> converter, IMapper<PunchModel, PunchEntity> mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _converter = converter;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAll([FromRoute] Guid volunteerId, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var volunteerPunches = (await unitOfWork
                        .GetRepository<IPunchRepository>()
                        .GetAllAsync(volunteerId)
                        .ToImmutableArrayAsync(cancellationToken))
                    .Select(_converter.Convert)
                    .ToImmutableArray();

                return View(volunteerPunches);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PunchModel punch, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IPunchRepository>();
                var entity = new PunchEntity();

                _mapper.MapTo(punch, entity);

                await repository.AddAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                punch = _converter.Convert(entity);

                return View(@"Edit", punch);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var repository = unitOfWork.GetRepository<IPunchRepository>();
                var entity = await repository.GetAsync(id, cancellationToken);
                var punch = _converter.Convert(entity);

                return View(punch);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] PunchModel punch, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IPunchRepository>();
                var entity = await repository.GetAsync(punch.Id, cancellationToken);

                _mapper.MapTo(punch, entity);

                await repository.UpdateAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                punch = _converter.Convert(entity);

                return View(punch);
            }
        }

    }
}