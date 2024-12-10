using Asp.Versioning.Builder;
using MicroServices.Catalog.Api.Features.Categories.Create;
using MicroServices.Catalog.Api.Features.Categories.GetAll;
using MicroServices.Catalog.Api.Features.Categories.GetById;

namespace MicroServices.Catalog.Api.Features.Categories
{
	public static class CategoryEndpointExt
	{
		public static void AddCategoryGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
		{
			//withtags ile swagger tarafinda course ve categories ile başlık halinde ayirdik
			app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories").WithApiVersionSet(apiVersionSet)
				.CreateCategoryGroupItemEndpoint()
				.GetAllCategoryGroupItemEndpoint()
				.GetByIdCategoryGroupItemEndpoint();
		}
	}
}
