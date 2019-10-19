using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	public abstract class AbstractIdentifiable
	{
		[Column("Id", TypeName = "UNIQUEIDENTIFIER")]
		public Guid ID { get; set; }
	}
}