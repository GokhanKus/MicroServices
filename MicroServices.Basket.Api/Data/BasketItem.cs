namespace MicroServices.Basket.Api.Data
{
	public class BasketItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public decimal? PriceByApplyDiscountRate { get; set; }

		public BasketItem(Guid id, string name, decimal price, string? imageUrl, decimal? priceByApplyDiscountRate)
		{
			Id = id;
			Name = name;
			Price = price;
			ImageUrl = imageUrl;
			PriceByApplyDiscountRate = priceByApplyDiscountRate;
		}
	}
}
