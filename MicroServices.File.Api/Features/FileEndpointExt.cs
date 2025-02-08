using Asp.Versioning.Builder;
using MicroServices.File.Api.Features.File.Delete;
using MicroServices.File.Api.Features.File.Upload;

namespace MicroServices.File.Api.Features.Courses
{
	public static class FileEndpointExt
	{
		public static void AddFileGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
		{
			app.MapGroup("api/v{version:apiVersion}/files").WithTags("files").WithApiVersionSet(apiVersionSet)
				.UploadFileGroupItemEndpoint()
				.DeleteFileGroupItemEndpoint();
		}
	}
}
