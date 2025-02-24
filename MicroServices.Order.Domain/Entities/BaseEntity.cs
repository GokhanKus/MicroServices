namespace MicroServices.Order.Domain.Entities
{
	public class BaseEntity<TEntityId>
	{
		public TEntityId Id { get; set; } = default!;
	}
}
