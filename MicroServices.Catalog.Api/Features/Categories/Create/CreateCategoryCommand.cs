using MediatR;
using MicroServices.Shared;

namespace MicroServices.Catalog.Api.Features.Categories.Create
{
	public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
}

//sonu command ile bitiyorsa bu create, update, delete'e karsilik gelen endpointlerimiz; query ile biterse get yani data almaya karsilik gelen endpointlerimizdir
