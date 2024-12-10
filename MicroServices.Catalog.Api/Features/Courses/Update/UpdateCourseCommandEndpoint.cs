using MicroServices.Catalog.Api.Features.Courses.Create;
using MicroServices.Shared.Filters;

namespace MicroServices.Catalog.Api.Features.Courses.Update
{
	public static class UpdateCourseCommandEndpoint
	{
		public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator)
				=> (await mediator.Send(command)).ToGenericResult())
				.WithName("UpdateCourse")
				.AddEndpointFilter<ValidationFilter<UpdateCourseCommandValidator>>();

			return group;
		}
	}
}
