using MicroServices.Order.Application.Contracts.Repositories;

namespace MicroServices.Order.Persistence.Repositories
{
	public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
	{
	
	}
}
