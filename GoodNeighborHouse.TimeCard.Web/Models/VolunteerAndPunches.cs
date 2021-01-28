using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public sealed class VolunteerAndPunches : IEnumerable<IGrouping<DateTime, IPunch>>
	{
		private readonly IImmutableList<IGrouping<DateTime, IPunch>> _punchesByDay;

		public VolunteerAndPunches(Volunteer volunteer, IEnumerable<IPunch> punches)
		{
			Volunteer = volunteer;

			_punchesByDay = punches
				.OrderBy(punch => punch, PunchComparer.Instance)
				.GroupBy(punch => punch.GetDate())
				.ToImmutableArray();
		}

		public Volunteer Volunteer { get; set; }

		public IEnumerator<IGrouping<DateTime, IPunch>> GetEnumerator()
		{
			return _punchesByDay.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}