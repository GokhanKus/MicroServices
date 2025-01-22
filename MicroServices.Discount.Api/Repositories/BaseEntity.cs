using MongoDB.Bson.Serialization.Attributes;

namespace MicroServices.Discount.Api.Repositories
{
	public class BaseEntity
	{
		[BsonElement("_id")]//mongodb'de bu _id olarak tutulsun
		public Guid Id { get; set; }

	}
}
