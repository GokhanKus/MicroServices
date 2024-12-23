using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Data;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.AddBasketItem
{
	public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
		{
			//var userId = Guid.NewGuid();
			var userId = identityService.UserId;
			var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

			var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			Data.Basket? currentBasket;

			var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, priceByApplyDiscountRate: null);

			if (string.IsNullOrEmpty(basketAsString))
			{
				currentBasket = new Data.Basket(userId, [newBasketItem]);
				await AddToRedis(cacheKey, currentBasket, cancellationToken);
				return ServiceResult.SuccessAsNoContent();
			}

			currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

			//client bir kursu birden fazla sepete ekleyemez, onun için asagida ufak bir business var sonradan refactor edilecek
			var existingBasketItem = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.CourseId);

			if (existingBasketItem is not null)
				currentBasket.Items.Remove(existingBasketItem);
			
			currentBasket.Items.Add(newBasketItem);
			currentBasket.ApplyAvailableDiscount(); //baskette 1 urun var indirim eklendi sonra tekrar ürün eklenince indirim o ürün icin gecerli olmuyor, bu satir o yuzden 

			await AddToRedis(cacheKey, currentBasket, cancellationToken);
			return ServiceResult.SuccessAsNoContent();

		}

		private async Task AddToRedis(string cacheKey, Data.Basket? currentBasket, CancellationToken cancellationToken)
		{
			var basketAsString = JsonSerializer.Serialize(currentBasket);
			await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
		}
	}
}
