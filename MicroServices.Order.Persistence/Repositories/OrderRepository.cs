using MicroServices.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroServices.Order.Persistence.Repositories
{
	public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
	{
		public Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid buyerId)
		{
			return Context.Orders.Include(o=>o.OrderItems).Where(o => o.BuyerId == buyerId).OrderByDescending(o=>o.Created).ToListAsync(); 
		}
	}
}
