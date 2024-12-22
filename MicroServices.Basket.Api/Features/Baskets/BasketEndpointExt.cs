using Asp.Versioning.Builder;
using MicroServices.Basket.Api.Features.Baskets.AddBasketItem;
using MicroServices.Basket.Api.Features.Baskets.DeleteBasketItem;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public static class BasketEndpointExt
	{
		public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
		{
			app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets").WithApiVersionSet(apiVersionSet)
				.AddBasketItemGroupItemEndpoint()
				.DeleteBasketItemGroupItemEndpoint();
		}
	}
}
