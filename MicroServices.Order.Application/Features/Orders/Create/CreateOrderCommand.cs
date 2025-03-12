using MicroServices.Shared;

namespace MicroServices.Order.Application.Features.Orders.Create
{
	public record CreateOrderCommand(float? DiscountRate, AddressDto Address, PaymentDto Payment, List<OrderItemDto> OrderItems):IRequestByServiceResult;
	public record AddressDto(string Province, string District, string Street, string ZipCode, string Line);
	public record PaymentDto(string CardHolderName, string CardNumber, string Expiration, string CVC, decimal Amount);
	public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);
}
