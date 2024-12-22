using System.Text.Json.Serialization;

namespace MicroServices.Basket.Api.Dto
{
	public record BasketDto
	{
		//TODO: userid response'da gorunmesin, ancak suan baskete 1 item eklenebiliyor yenisi eklenirsen önceki siliniyor bunu düzelt
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
