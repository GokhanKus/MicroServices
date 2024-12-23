using MediatR;
using MicroServices.Basket.Api.Data;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using System.Text.Json;

namespace MicroServices.Basket.Api.Features.Baskets.AddBasketItem
{
	public class AddBasketItemCommandHandler(IIdentityService identityService,BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
		{
			var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

			Data.Basket? currentBasket;

			var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, priceByApplyDiscountRate: null);

			if (string.IsNullOrEmpty(basketAsJson))
			{
				currentBasket = new Data.Basket(identityService.UserId, [newBasketItem]);
				await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
				return ServiceResult.SuccessAsNoContent();
			}

			currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

			//client bir kursu birden fazla sepete ekleyemez, onun için asagida ufak bir business var sonradan refactor edilecek
			var existingBasketItem = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.CourseId);

			if (existingBasketItem is not null)
				currentBasket.Items.Remove(existingBasketItem);
			
			currentBasket.Items.Add(newBasketItem);
			currentBasket.ApplyAvailableDiscount(); //baskette 1 urun var indirim eklendi sonra tekrar ürün eklenince indirim o ürün icin gecerli olmuyor, bu satir o yuzden 

			await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}
