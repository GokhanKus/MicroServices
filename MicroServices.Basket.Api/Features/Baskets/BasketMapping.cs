using AutoMapper;
using MicroServices.Basket.Api.Data;
using MicroServices.Basket.Api.Dto;

namespace MicroServices.Basket.Api.Features.Baskets
{
	public class BasketMapping:Profile
	{
		public BasketMapping()
		{
			CreateMap<BasketDto, Data.Basket>().ReverseMap();
			CreateMap<BasketItemDto, BasketItem>().ReverseMap();
		}
	}
}
