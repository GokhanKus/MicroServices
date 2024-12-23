using AutoMapper;
using MediatR;
using MicroServices.Basket.Api.Dto;
using MicroServices.Shared;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.GetBasket
{
	public class GetBasketQueryHandler(BasketService basketService, IMapper mapper)
		: IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
	{
		public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
		{
			var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

			if (string.IsNullOrEmpty(basketAsString))
				return ServiceResult<BasketDto>.Error("basket not found", HttpStatusCode.NotFound);

			var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;
			var basketDto = mapper.Map<BasketDto>(basket);

			return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
		}
	}
}
