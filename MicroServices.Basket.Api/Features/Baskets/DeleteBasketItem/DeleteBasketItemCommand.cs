using MicroServices.Shared;

namespace MicroServices.Basket.Api.Features.Baskets.DeleteBasketItem
{
	public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult;
}
