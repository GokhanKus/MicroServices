using MediatR;
using MicroServices.Shared;
using Microsoft.Extensions.FileProviders;

namespace MicroServices.File.Api.Features.File.Upload
{
	//file'in kaydedilecegi fiziksel yolu anlık olarak ogrenmemiz lazım, cunku baska bir zaman clouda kaydedilecegi zaman bu path degisebilir.
	public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
	{
		public Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
