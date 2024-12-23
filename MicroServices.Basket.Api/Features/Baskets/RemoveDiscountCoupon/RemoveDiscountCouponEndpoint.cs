using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Dto;
using MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroServices.Shared;
using MicroServices.Shared.Extensions;
using MicroServices.Shared.Filters;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
	public record RemoveDiscountCouponCommand:IRequestByServiceResult;

	public class RemoveDiscountCouponHandler(IIdentityService identityService, IDistributedCache distributedCache) 
		: IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
		{
			var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.UserId);
			var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			if (string.IsNullOrEmpty(basketAsString))
				return ServiceResult.Error("basket not found", HttpStatusCode.NotFound);

			var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

			basket.ClearDiscount();

			basketAsString = JsonSerializer.Serialize(basket);
			await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

			return ServiceResult.SuccessAsNoContent();
		}
	}
	public static class RemoveDiscountCouponEndpoint
	{
		public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapDelete("/remove-discount-coupon", async (IMediator mediator)
				=> (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult()) 
				.WithName("RemoveDiscountCoupon")
				.MapToApiVersion(1, 0);

			return group;
		}
	}
}
