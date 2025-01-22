using Asp.Versioning.Builder;
using MicroServices.Discount.Api.Features.Discounts.Create;
using MicroServices.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public static class DiscountEndpointExt
	{
		public static void AddDiscountGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
		{
			app.MapGroup("api/v{version:apiVersion}/discount").WithTags("Discounts").WithApiVersionSet(apiVersionSet)
				.CreateDiscountGroupItemEndpoint()
				.GetDiscountByCodeGroupItemEndpoint();
		}
	}
}
