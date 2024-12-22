using MediatR;
using MicroServices.Basket.Api.Dto;
using MicroServices.Shared;

namespace MicroServices.Basket.Api.Features.Baskets.GetBasket
{
	public class GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
