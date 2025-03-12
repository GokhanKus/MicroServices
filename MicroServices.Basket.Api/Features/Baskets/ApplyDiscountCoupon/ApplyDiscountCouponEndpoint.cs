using MediatR;
using MicroServices.Shared.Extensions;
using MicroServices.Shared.Filters;

namespace MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
	public static class ApplyDiscountCouponEndpoint
	{
		public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator)
				=> (await mediator.Send(command)).ToGenericResult())
				.WithName("ApplyDiscountCoupon")
				.AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>()
				.MapToApiVersion(1, 0);

			return group;
		}
	}
}
