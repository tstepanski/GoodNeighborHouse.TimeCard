using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunchController
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PunchModel>> Create(Guid volunteerId, bool isClockIn, long forTimeinMilliseconds, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IPunchRepository>();
                var entity = new PunchEntity();
                var punch = new PunchModel()
                {
                    IsClockIn = isClockIn,
                    PunchTime = new DateTime(forTimeinMilliseconds),
                    VolunteerId = volunteerId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System", //TODO - get User.Identity.Name,
                    UpdatedAt = DateTime.Now,
                    LastUpdatedBy = "System", //TODO - get User.Identity.Name,
                };
                _mapper.MapTo(punch, entity);

                await repository.AddAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                punch = _converter.Convert(entity);
                return punch;
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PunchModel>> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IPunchRepository>();
                await repository.DeleteAsync(id);
                await unitOfWork.CommitAsync(cancellationToken);

                return new AcceptedResult();
            }

        }

    }
}
