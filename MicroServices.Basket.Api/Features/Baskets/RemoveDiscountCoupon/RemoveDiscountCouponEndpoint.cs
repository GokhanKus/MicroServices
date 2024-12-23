using MediatR;
using MicroServices.Shared;
using MicroServices.Shared.Extensions;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
	public record RemoveDiscountCouponCommand:IRequestByServiceResult;

	public class RemoveDiscountCouponHandler(BasketService basketService) 
		: IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
		{
			var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

			if (string.IsNullOrEmpty(basketAsJson))
				return ServiceResult.Error("basket not found", HttpStatusCode.NotFound);

			var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

			basket.ClearDiscount();

			await basketService.CreateBasketCacheAsync(basket,cancellationToken);

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
