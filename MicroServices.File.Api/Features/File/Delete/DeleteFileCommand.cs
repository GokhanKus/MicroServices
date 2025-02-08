using MediatR;
using MicroServices.Shared;

namespace MicroServices.File.Api.Features.File.Delete
{
	public record DeleteFileCommand(string FileName):IRequestByServiceResult;
}
