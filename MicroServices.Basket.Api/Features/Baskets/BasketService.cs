using MicroServices.Basket.Api.Const;
using MicroServices.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public class BasketService(IIdentityService identityService,IDistributedCache distributedCache)
	{
		private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.UserId);
		public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
		{
			var basketAsString = distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
			return basketAsString;
		}
		public async Task CreateBasketCacheAsync(Data.Basket? currentBasket, CancellationToken cancellationToken)
		{
			var basketAsString = JsonSerializer.Serialize(currentBasket);
			await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
		}
	}
}
