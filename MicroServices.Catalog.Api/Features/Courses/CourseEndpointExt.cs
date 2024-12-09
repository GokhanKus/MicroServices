using MicroServices.Catalog.Api.Features.Categories.Create;
using MicroServices.Catalog.Api.Features.Courses.Create;
using MicroServices.Catalog.Api.Features.Courses.GetAll;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public static class CourseEndpointExt
	{
		public static void AddCourseGroupEndpointExt(this WebApplication app)
		{
			app.MapGroup("api/courses").WithTags("Courses")
				.CreateCourseGroupItemEndpoint()
				.GetAllCoursesGroupItemEndpoint();
		}
	}
}
