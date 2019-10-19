using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;
using OrganizationModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<OrganizationEntity, OrganizationModel> _converter;
        private readonly IMapper<OrganizationModel, OrganizationEntity> _mapper;

        public OrganizationController(IUnitOfWorkFactory unitOfWorkFactory,
            IConverter<OrganizationEntity, OrganizationModel> converter, IMapper<OrganizationModel, OrganizationEntity> mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _converter = converter;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAll(CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var organizations = (await unitOfWork
                        .GetRepository<IOrganizationRepository>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken))
                    .Select(_converter.Convert)
                    .ToImmutableArray();

                return View(organizations);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganizationModel organization, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IOrganizationRepository>();
                var entity = new OrganizationEntity();

                _mapper.MapTo(organization, entity);

                await repository.AddAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                organization = _converter.Convert(entity);

                return View(@"Edit", organization);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var repository = unitOfWork.GetRepository<IOrganizationRepository>();
                var entity = await repository.GetAsync(id, cancellationToken);
                var organization = _converter.Convert(entity);

                return View(organization);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] OrganizationModel organization,
            CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IOrganizationRepository>();
                var entity = await repository.GetAsync(organization.Id, cancellationToken);

                _mapper.MapTo(organization, entity);

                await repository.UpdateAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                organization = _converter.Convert(entity);

                return View(organization);
            }
        }
    }
}