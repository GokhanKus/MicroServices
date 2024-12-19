namespace MicroServices.Basket.Api.Dto
{
	public record BasketItemDto(Guid Id, string Name, decimal Price, string ImageUrl, decimal? PriceByApplyDiscountRate);//CourseId CourseName ...
}
