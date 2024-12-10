using MicroServices.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Catalog.Api.Features.Courses.Create
{
	public static class CreateCourseCommandEndpoint
	{
		public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (CreateCourseCommand command, IMediator mediator)
				=> (await mediator.Send(command)).ToGenericResult())
				.WithName("CreateCourse")
				.Produces<Guid>(StatusCodes.Status201Created)
				.Produces(StatusCodes.Status404NotFound)
				.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
				.Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
				.AddEndpointFilter<ValidationFilter<CreateCourseCommandValidator>>();

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