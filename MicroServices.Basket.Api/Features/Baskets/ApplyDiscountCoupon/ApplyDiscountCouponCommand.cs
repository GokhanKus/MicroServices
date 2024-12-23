using MicroServices.Shared;

namespace MicroServices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
	public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
}
