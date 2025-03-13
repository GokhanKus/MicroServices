using AutoMapper;
using MediatR;
using MicroServices.Order.Application.Contracts.Repositories;
using MicroServices.Order.Application.Features.Orders.Create;
using MicroServices.Order.Domain.Entities;
using MicroServices.Shared;
using MicroServices.Shared.Services;

namespace MicroServices.Order.Application.Features.Orders.GetOrders
{
	public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository,IMapper mapper)
		: IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
	{
		public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			var orders = await orderRepository.GetOrdersByUserId(identityService.UserId);
			var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();
			return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
		}
	}
}
