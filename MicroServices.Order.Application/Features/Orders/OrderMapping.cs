using AutoMapper;
using MicroServices.Order.Application.Features.Orders.Create;
using MicroServices.Order.Domain.Entities;

namespace MicroServices.Order.Application.Features.Orders
{
	internal class OrderMapping:Profile
	{
		public OrderMapping()
		{
			CreateMap<OrderItem, OrderItemDto>().ReverseMap();
		}
	}
}
