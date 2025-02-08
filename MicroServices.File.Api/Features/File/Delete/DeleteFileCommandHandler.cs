using MediatR;
using MicroServices.Shared;
using Microsoft.Extensions.FileProviders;

namespace MicroServices.File.Api.Features.File.Delete
{
	public record DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
	{
		public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
		{
			//bu metottda await kullanmaya gerek olmadıgı icin Task.FromResult ile task donebiliriz

			//file exist or not?
			var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileName));
			if (!fileInfo.Exists)
			{
				return Task.FromResult(ServiceResult.ErrorAsNotFound());
			}

			//delete file
			System.IO.File.Delete(fileInfo.PhysicalPath!);
			return Task.FromResult(ServiceResult.SuccessAsNoContent());
		}
	}
}
