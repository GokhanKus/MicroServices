using MediatR;
using MicroServices.Shared;
using Microsoft.Extensions.FileProviders;

namespace MicroServices.File.Api.Features.File.Upload
{
	//file'in kaydedilecegi fiziksel yolu anlık olarak ogrenmemiz lazım, cunku baska bir zaman clouda kaydedilecegi zaman bu path degisebilir.
	public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
	{
		public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
		{
			if (request.File is null || request.File.Length == 0)
				return ServiceResult<UploadFileCommandResponse>.Error("Invalid file", System.Net.HttpStatusCode.BadRequest);
			
			var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
			var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);

			await using var stream = new FileStream(uploadPath, FileMode.Create);
			await request.File.CopyToAsync(stream, cancellationToken);

			var response = new UploadFileCommandResponse(newFileName, $"files/{newFileName}", request.File.FileName);
			return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response,response.FilePath);
		}
	}
}
