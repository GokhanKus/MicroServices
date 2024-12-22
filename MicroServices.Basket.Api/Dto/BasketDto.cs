using System.Text.Json.Serialization;

namespace MicroServices.Basket.Api.Dto
{
	public record BasketDto
	{
		// primary ctor'da attribute yazilamadigi icin (JsonIgnore orn) bu sekilde acik acik yazdik
		[JsonIgnore] public Guid UserId { get; init; }
		public List<BasketItemDto> Items { get; set; } = new();
		public BasketDto(Guid userId, List<BasketItemDto> items)
		{
			UserId = userId;
			Items = items;
		}
		public BasketDto()
		{

		}
	}
	//public record BasketDto(Guid UserId, List<BasketItemDto> Items);

}
