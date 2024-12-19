using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Dto;
using MicroServices.Shared;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.AddBasketItem
{
	public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
		{
			// TODO : userId tokendan gelecek, ancak henüz bu mekanizma olmadigi icin simdlik temsili olarak bir tane random userId uretelim
			var userId = Guid.NewGuid(); 
			var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

			var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			BasketDto? currentBasket;

			var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, PriceByApplyDiscountRate: null);

			if (string.IsNullOrEmpty(basketAsString))
			{
				currentBasket = new BasketDto(userId, [newBasketItem]);
			}
			else
			{
				currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

				//client bir kursu birden fazla sepete ekleyemez, onun için asagida ufak bir business var sonradan refactor edilecek
				var existingBasketItem = currentBasket.BasketItems.FirstOrDefault(bi => bi.Id == request.CourseId);
				if (existingBasketItem is not null)
				{
					currentBasket.BasketItems.Remove(existingBasketItem);
					currentBasket.BasketItems.Add(newBasketItem);
				}
				else
				{
					currentBasket.BasketItems.Add(newBasketItem);
				}

			}
			basketAsString = JsonSerializer.Serialize(currentBasket);
			await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}
