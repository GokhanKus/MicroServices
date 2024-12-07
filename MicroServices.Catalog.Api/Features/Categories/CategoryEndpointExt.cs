using MicroServices.Catalog.Api.Features.Categories.Create;
using MicroServices.Catalog.Api.Features.Categories.GetAll;

namespace MicroServices.Catalog.Api.Features.Categories
{
	public static class CategoryEndpointExt
	{
		public static void AddCategoryGroupEndpointExt(this WebApplication app)
		{
			app.MapGroup("api/categories")
				.CreateCategoryGroupItemEndpoint()
				.GetAllCategoryGroupItemEndpoint();
		}
	}
}
