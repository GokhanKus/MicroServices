using MicroServices.Shared;

namespace MicroServices.Basket.Api.Features.Baskets.AddBasketItem
{
	public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string ImageUrl) : IRequestByServiceResult;
}
