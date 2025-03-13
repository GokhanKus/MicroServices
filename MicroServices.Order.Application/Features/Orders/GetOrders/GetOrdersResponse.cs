using MicroServices.Order.Application.Features.Orders.Create;

namespace MicroServices.Order.Application.Features.Orders.GetOrders
{
	public record GetOrdersResponse(DateTime Created,decimal TotalPrice,List<OrderItemDto>OrderItems);
}
