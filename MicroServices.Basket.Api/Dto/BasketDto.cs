using System.Text.Json.Serialization;

namespace MicroServices.Basket.Api.Dto
{
	public record BasketDto(List<BasketItemDto> Items)
	{
		[JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
		public float? DiscountRate { get; set; }
		public string? Coupon { get; set; }
		public decimal TotalPrice => Items.Sum(i => i.Price);
		public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount ? null : Items.Sum(i => i.PriceByApplyDiscountRate);
	}
}
