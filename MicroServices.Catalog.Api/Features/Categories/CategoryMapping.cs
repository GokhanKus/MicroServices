using AutoMapper;
using MicroServices.Catalog.Api.Features.Categories.Dtos;

namespace MicroServices.Catalog.Api.Features.Categories
{
	public class CategoryMapping:Profile
	{
		public CategoryMapping()
		{
			CreateMap<Category, CategoryDto>().ReverseMap(); 
		}
	}
}
