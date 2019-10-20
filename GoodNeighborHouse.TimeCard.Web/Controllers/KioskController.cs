using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public sealed class KioskController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IConverter<VolunteerEntity, VolunteerModel> _volunteerConverter;

		public KioskController(IUnitOfWorkFactory unitOfWorkFactory,
			IConverter<VolunteerEntity, VolunteerModel> volunteerConverter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_volunteerConverter = volunteerConverter;
		}

		[HttpPost(@"User/{username}")]
		public async Task<IActionResult> SelectUser([FromRoute] string username,
			CancellationToken cancellationToken = default)
		{
			username = username.Trim();

			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();
				var entity = await repository.GetByUserNameAsync(username, cancellationToken);
				var volunteer = _volunteerConverter.Convert(entity);

				return View(@"Punch", volunteer);
			}
		}

		public Task<IActionResult> Punch()
		{
			return null;
		}
	}
}