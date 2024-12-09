using MicroServices.Catalog.Api.Features.Categories.Create;
using MicroServices.Catalog.Api.Features.Categories.GetAll;
using MicroServices.Catalog.Api.Features.Categories.GetById;

namespace MicroServices.Catalog.Api.Features.Categories
{
	public static class CategoryEndpointExt
	{
		public static void AddCategoryGroupEndpointExt(this WebApplication app)
		{
			//withtags ile swagger tarafinda course ve categories ile başlık halinde ayirdik
			app.MapGroup("api/categories").WithTags("Categories")
				.CreateCategoryGroupItemEndpoint()
				.GetAllCategoryGroupItemEndpoint()
				.GetByIdCategoryGroupItemEndpoint();
		}
	}
}
