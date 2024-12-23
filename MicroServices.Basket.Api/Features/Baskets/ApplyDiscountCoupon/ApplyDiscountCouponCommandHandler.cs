using MediatR;
using MicroServices.Basket.Api.Dto;
using MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroServices.Shared;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public class ApplyDiscountCouponCommandHandler(BasketService basketService)
		: IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
		{
			var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

			if (string.IsNullOrEmpty(basketAsJson))
				return ServiceResult<BasketDto>.Error("basket not found", HttpStatusCode.NotFound);

			var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

			if (!basket.Items.Any()) //sepette urun yoksa eklenecek bir kupon da olamaz..
				return ServiceResult<BasketDto>.Error("basket item not found", HttpStatusCode.NotFound);

			basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

			await basketService.CreateBasketCacheAsync(basket, cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}