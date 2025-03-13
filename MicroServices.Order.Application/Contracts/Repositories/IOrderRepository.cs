namespace MicroServices.Order.Application.Contracts.Repositories
{
	public interface IOrderRepository:IGenericRepository<Guid,Domain.Entities.Order>
	{
		//ilerde buraya yardımcı metotlar gelecek orn orderları almak ile ilgili
		public Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid buyerId);
	}
}
