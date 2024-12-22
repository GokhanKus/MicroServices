using MediatR;
using MicroServices.Basket.Api.Const;
using MicroServices.Basket.Api.Dto;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.DeleteBasketItem
{
	public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
		{
			var userId = identityService.UserId;
			var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

			var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

			if (string.IsNullOrEmpty(basketAsString))
				return ServiceResult.Error("basket not found", HttpStatusCode.NotFound);

			var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
			var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(b => b.Id == request.Id);

			if (basketItemToDelete is null)
				return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);

			currentBasket.BasketItems.Remove(basketItemToDelete);
			basketAsString = JsonSerializer.Serialize(currentBasket);
			await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}
