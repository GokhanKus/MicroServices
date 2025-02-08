using MediatR;
using MicroServices.File.Api.Features.File.Upload;
using MicroServices.Shared.Extensions;
using MicroServices.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.File.Api.Features.Discounts.Create
{
	public static class UploadFileCommandEndpoint
	{
		public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (IFormFile file, IMediator mediator)
				=> (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
				.WithName("upload")
				.MapToApiVersion(1, 0)
				.Produces<Guid>(StatusCodes.Status201Created)
				.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
				.Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
				.DisableAntiforgery();

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
#region UseAntiforgery
/* CSRF(Cross Site Request Forgery) genel yapı olarak bir web sitesinin açığından faydalanarak 
site kullanıcılarının istekleri dışında sanki o kullanıcıymış gibi erişerek işlem yapılması sürecini içerir.
Genellikle GET requestleri ve SESSION işlemlerinin doğru kontrol edilememesi durumlarındaki açıklardan saldırganların faydalanmasını sağlamaktadır. */
#endregion