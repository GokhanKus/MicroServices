using MediatR;
using MicroServices.Shared;

namespace MicroServices.Order.Application.Features.Orders.GetOrders
{
	public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
	
}
