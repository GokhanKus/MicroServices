using MicroServices.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Discount.Api.Features.Discounts.Create
{
	public static class CreateDiscountCommandEndpoint
	{
		public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator)
				=> (await mediator.Send(command)).ToGenericResult())
				.WithName("CreateDiscount")
				.MapToApiVersion(1, 0)
				.Produces<Guid>(StatusCodes.Status201Created)
				.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
				.Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
				.AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

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