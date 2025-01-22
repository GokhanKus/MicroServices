using MongoDB.Bson.Serialization.Attributes;

namespace MicroServices.Discount.Api.Repositories
{
	public class BaseEntity
	{
		public Guid Id { get; set; }
	}
}
