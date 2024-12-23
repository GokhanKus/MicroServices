using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Dto;
using MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache)
		: IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
		{
			var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.UserId);
			var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			if (string.IsNullOrEmpty(basketAsJson))
				return ServiceResult<BasketDto>.Error("basket not found", HttpStatusCode.NotFound);

			var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

			if(!basket.Items.Any()) //sepette urun yoksa eklenecek bir kupon da olamaz..
				return ServiceResult<BasketDto>.Error("basket item not found", HttpStatusCode.NotFound);

			basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

			basketAsJson = JsonSerializer.Serialize(basket);
			await distributedCache.SetStringAsync(cacheKey, basketAsJson,cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}