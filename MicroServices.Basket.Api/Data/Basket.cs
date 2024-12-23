using System.Text.Json.Serialization;

namespace MicroServices.Basket.Api.Data
{
	//anamic model = rich domain model (behaviour + data)  
	public class Basket
	{
		public Basket(Guid userId, List<BasketItem> items)
		{
			UserId = userId;
			Items = items;
		}
		public Basket()
		{
			
		}
		public Guid UserId { get; init; }
		public List<BasketItem> Items { get; set; } = new();
		public float? DiscountRate { get; set; }
		public string? Coupon { get; set; }

		[JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
		[JsonIgnore] public decimal TotalPrice => Items.Sum(i => i.Price);
		[JsonIgnore] public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount ? null : Items.Sum(i => i.PriceByApplyDiscountRate);

		public void ApplyNewDiscount(string coupon, float discountRate)
		{
			Coupon = coupon;
			DiscountRate = discountRate;

			foreach (var basketItem in Items)
			{
				basketItem.PriceByApplyDiscountRate = basketItem.Price * (decimal)(1 - discountRate);
			}
		}
		public void ApplyAvailableDiscount()
		{
			if (!IsApplyDiscount) return;
			foreach (var basketItem in Items)
			{
				basketItem.PriceByApplyDiscountRate = basketItem.Price * (decimal)(1 - DiscountRate!);
			}
		}
		public void ClearDiscount()
		{
			DiscountRate = null;
			Coupon = null;
			foreach (var basketItem in Items)
			{
				basketItem.PriceByApplyDiscountRate = null;
			}
		}
	}
}
