using MicroServices.Shared;

namespace MicroServices.File.Api.Features.File.Upload
{
	public record UploadFileCommand(IFormFile file):IRequestByServiceResult<UploadFileCommandResponse>;
}
