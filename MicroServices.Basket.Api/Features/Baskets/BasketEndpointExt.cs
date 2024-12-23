using Asp.Versioning.Builder;
using MicroServices.Basket.Api.Features.Baskets.AddBasketItem;
using MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroServices.Basket.Api.Features.Baskets.DeleteBasketItem;
using MicroServices.Basket.Api.Features.Baskets.GetBasket;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public static class BasketEndpointExt
	{
		public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
		{
			app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets").WithApiVersionSet(apiVersionSet)
				.AddBasketItemGroupItemEndpoint()
				.DeleteBasketItemGroupItemEndpoint()
				.GetBasketGroupItemEndpoint()
				.ApplyDiscountCouponGroupItemEndpoint();
		}
	}
}
