namespace MicroServices.Order.Domain.Entities
{
	public class OrderItem:BaseEntity<int>
	{
		public Guid ProductId { get; set; } //product = kursumuz, yarın birgun baska bir sey de satılabilir o yuzden hepsi birer product olsun 
		public string ProductName { get; set; } = null!;
		public decimal UnitPrice { get; set; }
		public Guid OrderId { get; set; } 
		public Order Order { get; set; } = null!;

		//behaviour methods - Yardımcı methodlar (Rich Domain Model)
		public void SetItem(Guid productId, string productName, decimal unitPrice)
		{
			if (string.IsNullOrEmpty(ProductName)) throw new ArgumentNullException(nameof(ProductName),"product name cannot be empty");
			if (unitPrice <= 0) throw new ArgumentOutOfRangeException("unit price must be greater than 0");

			ProductId = productId;
			ProductName = productName;
			UnitPrice = unitPrice;
		}
		public void UpdatePrice(decimal newPrice)
		{
			if(newPrice <= 0) throw new ArgumentOutOfRangeException(nameof(UnitPrice), "unit price must be greater than 0");
			UnitPrice = newPrice;
		}
		public void ApplyDiscount(float discountPercentage)
		{
			if(discountPercentage < 0 || discountPercentage >= 100)
				throw new ArgumentOutOfRangeException("discount percentage must be between 0 and 100");

			UnitPrice -= (UnitPrice * (decimal)discountPercentage / 100);
		}
		public bool IsSameItem(OrderItem otherItem) 
			=> ProductId == otherItem.ProductId;
	}
}

#region Anemic Model & Rich Domain Model fark
/*
Projemizde entity'miz icerisinde sadece propertylerimiz varsa bu anemic modeldir,
ama entity icerisinde yardımcı metotlar varsa(behaviour methods) bu rich domain modeldir.
 */
#endregion
