using Asp.Versioning.Builder;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public static class FileEndpointExt
	{
		public static void AddFileGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
		{
			app.MapGroup("api/v{version:apiVersion}/files").WithTags("Discounts").WithApiVersionSet(apiVersionSet);
				
		}
	}
}
