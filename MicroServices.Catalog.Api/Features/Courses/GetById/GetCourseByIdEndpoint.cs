using MicroServices.Catalog.Api.Features.Courses.Dtos;

namespace MicroServices.Catalog.Api.Features.Courses.GetById
{
	public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;
	public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
	{
		public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
		{
			var course = await context.Courses.FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken);

			if (course == null)
			{
				return ServiceResult<CourseDto>.Error("Course not found",
					$"The course with id({request.Id}) was not found", HttpStatusCode.NotFound);
			}

			var category = await context.Categories.FindAsync(course.CategoryId, cancellationToken);
			course.Category = category!;

			var courseAsDto = mapper.Map<CourseDto>(course);
			return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
		}
	}
	public static class GetCourseByIdEndpoint
	{
		public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/{id:guid}", async (Guid id, IMediator mediator)
				=> (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
				.WithName("GetByIdCourse")
				.MapToApiVersion(1, 0);

			return group;
		}
	}
}
