using MassTransit;
using System.Security.Cryptography;
using System.Text;

namespace MicroServices.Order.Domain.Entities
{
	public class Order:BaseEntity<Guid>
	{
		public string Code { get; set; } = null!;
		public DateTime Created { get; set; }
		public Guid BuyerId { get; set; }
		public OrderStatus Status { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; } = null!;
		public decimal TotalPrice { get; set; }
		public float? DiscountRate { get; set; }
		public Guid PaymentId { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new();

		//behaviour methods - Yardımcı methodlar (Rich Domain Model)
		public static string GenerateCode()
		{
			var orderCode = new StringBuilder(10);
			var rng = RandomNumberGenerator.Create();
			var byteArray = new byte[1];

			for (int i = 0; i < 10; i++)
			{
				rng.GetBytes(byteArray);
				int digit = byteArray[0] % 10; // 0-9 arası rakam üret
				orderCode.Append(digit);
			}

			return orderCode.ToString();
		}
		public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
		{
			return new Order
			{
				Id = NewId.NextGuid(),
				Code = GenerateCode(),
				AddressId = addressId,
				BuyerId = buyerId,
				Created = DateTime.Now,
				Status = OrderStatus.WaitingForPayment,
				TotalPrice = 0,
				DiscountRate = discountRate
			};
		}
		public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
		{
			var orderItem = new OrderItem();
			orderItem.SetItem(productId, productName, unitPrice);
			OrderItems.Add(orderItem);

			CalculateTotalPrice();
		}
		public void ApplyDiscount(float discountPercentage)
		{
			if (discountPercentage < 0 || discountPercentage >= 100)
				throw new ArgumentOutOfRangeException("discount percentage must be between 0 and 100");
			DiscountRate = discountPercentage;
			CalculateTotalPrice();
		}
		public void SetPaidStatus(Guid paymentId)
		{
			Status = OrderStatus.Paid;
			PaymentId = paymentId;
		}
		private void CalculateTotalPrice()
		{
			TotalPrice = OrderItems.Sum(x => x.UnitPrice);

			if (DiscountRate.HasValue)
				TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
		}
	}
}
