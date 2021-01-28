using System;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public sealed class PunchComparer : IComparer<IPunch>
	{
		private PunchComparer()
		{
		}

		public static IComparer<IPunch> Instance { get; } = new PunchComparer();

		public int Compare(IPunch left, IPunch right)
		{
			var leftValue = GetValue(left);
			var rightValue = GetValue(right);

			return leftValue.CompareTo(rightValue);
		}

		private DateTime GetValue(IPunch punch)
		{
			return punch switch
			{
				MissingPunch missingPunch => missingPunch.ShouldBeBefore.AddSeconds(-1),
				Punch realPunch => realPunch.PunchTime,
				_ => throw new ArgumentException($@"Invalid {nameof(IPunch)} type", nameof(punch))
			};
		}
	}
}