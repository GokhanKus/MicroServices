using MicroServices.Catalog.Api.Features.Courses.Dtos;

namespace MicroServices.Catalog.Api.Features.Courses.GetAllByUserId
{
	public record GetCoursesByUserIdQuery(Guid UserId) : IRequestByServiceResult<List<CourseDto>>;
	public class GetCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
	{
		public async Task<ServiceResult<List<CourseDto>>> Handle(GetCoursesByUserIdQuery request, CancellationToken cancellationToken)
		{
			var courses = await context.Courses.Where(x => x.UserId == request.UserId).ToListAsync();

			if (courses == null)
			{
				return ServiceResult<List<CourseDto>>.Error("Course not found",
					$"The course with id({request.UserId}) was not found", HttpStatusCode.NotFound);
			}

			var categories = await context.Categories.ToListAsync(cancellationToken);

			foreach (var course in courses)
			{
				course.Category = categories.First(c => c.Id == course.CategoryId);
			}

			var courseAsDto = mapper.Map<List<CourseDto>>(courses);
			return ServiceResult<List<CourseDto>>.SuccessAsOk(courseAsDto);
		}
	}
	public static class GetCoursesByIdEndpoint
	{
		public static RouteGroupBuilder GetByUserIdCoursesGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/user/{userId:guid}", async (Guid userId, IMediator mediator)
				=> (await mediator.Send(new GetCoursesByUserIdQuery(userId))).ToGenericResult())
				.WithName("GetByUserIdCourses");

			return group;
		}
	}
}
