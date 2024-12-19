namespace MicroServices.Basket.Api.Dto
{
	public record BasketDto(Guid UserId, List<BasketItemDto> BasketItems);
}
