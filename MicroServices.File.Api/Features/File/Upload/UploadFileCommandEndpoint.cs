using MediatR;
using MicroServices.File.Api.Features.File.Upload;
using MicroServices.Shared.Extensions;
using MicroServices.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Discount.Api.Features.Discounts.Create
{
	public static class UploadFileCommandEndpoint
	{
		public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (UploadFileCommand command, IMediator mediator)
				=> (await mediator.Send(command)).ToGenericResult())
				.WithName("UploadFile")
				.MapToApiVersion(1, 0)
				.Produces<Guid>(StatusCodes.Status201Created)
				.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
				.Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
				.AddEndpointFilter<ValidationFilter<UploadFileCommand>>();

			return group;
		}
	}
}
#region Produces
/*
Produces metodu swagger tarafinda client'i bilgilendirmek amaclidir ornegin CreateCourse endpointine request atarken alabilecegi responseları gosterir
ornegin geriye guid donen 201 durum kodlu success created alabilecegini belirtiyoruz ya da geriye problem details donen 400 bad request vs..
 */
#endregion