using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Dto;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.AddBasketItem
{
	public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
		{
			//var userId = Guid.NewGuid();
			var userId = identityService.UserId;
			var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

			var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			BasketDto? currentBasket;

			var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, PriceByApplyDiscountRate: null);

			if (string.IsNullOrEmpty(basketAsString))
			{
				currentBasket = new BasketDto(userId, [newBasketItem]);
				await AddToRedis(cacheKey, currentBasket, cancellationToken);
				return ServiceResult.SuccessAsNoContent();

			}

			currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

			//client bir kursu birden fazla sepete ekleyemez, onun için asagida ufak bir business var sonradan refactor edilecek
			var existingBasketItem = currentBasket!.BasketItems.FirstOrDefault(bi => bi.Id == request.CourseId);
			if (existingBasketItem is not null)
			{
				currentBasket.BasketItems.Remove(existingBasketItem);
			}

			currentBasket.BasketItems.Add(newBasketItem);
			await AddToRedis(cacheKey, currentBasket, cancellationToken);
			return ServiceResult.SuccessAsNoContent();

		}

		private async Task AddToRedis(string cacheKey, BasketDto? currentBasket, CancellationToken cancellationToken)
		{
			var basketAsString = JsonSerializer.Serialize(currentBasket);
			await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
		}
	}
}
