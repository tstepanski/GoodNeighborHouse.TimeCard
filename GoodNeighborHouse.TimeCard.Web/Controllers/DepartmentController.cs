using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public sealed class DepartmentController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<DepartmentEntity, DepartmentModel> _converter;
        private readonly IMapper<DepartmentModel, DepartmentEntity> _mapper;

        public DepartmentController(IUnitOfWorkFactory unitOfWorkFactory,
            IConverter<DepartmentEntity, DepartmentModel> converter, IMapper<DepartmentModel, DepartmentEntity> mapper)
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
                var departments = (await unitOfWork
                    .GetRepository<IDepartmentRepository>()
                    .GetAllAsync()
                    .ToImmutableArrayAsync(cancellationToken))
                    .Select(_converter.Convert)
                    .OrderBy(x => x.Name.ToLowerInvariant())
                    .ToImmutableArray();

                return View(departments);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var repository = unitOfWork.GetRepository<IDepartmentRepository>();
                var entity = await Task.Run(() => new DepartmentEntity());

                var department = _converter.Convert(entity);

                return View(@"Edit", department);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentModel department, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IDepartmentRepository>();
                var entity = new DepartmentEntity();

                _mapper.MapTo(department, entity);

                await repository.AddAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                department = _converter.Convert(entity);

                return RedirectToAction(@"ViewAll");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var repository = unitOfWork.GetRepository<IDepartmentRepository>();
                var entity = await repository.GetAsync(id, cancellationToken);

                if (entity == null)
                {
                    return NotFound();
                }

                var department = _converter.Convert(entity);

                return View(department);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, [Bind("Id,Name")] DepartmentModel department,
            CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IDepartmentRepository>();
                var entity = await repository.GetAsync(department.Id, cancellationToken);

                if (entity == null)
                {
                    entity = new DepartmentEntity();
                }

                _mapper.MapTo(department, entity);

                await repository.UpdateAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                department = _converter.Convert(entity);

                return RedirectToAction(@"ViewAll");
            }
        }
    }
}