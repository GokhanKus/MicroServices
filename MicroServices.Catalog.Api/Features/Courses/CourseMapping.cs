using MicroServices.Catalog.Api.Features.Courses.Create;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public class CourseMapping : Profile
	{
		public CourseMapping()
		{
			CreateMap<CreateCourseCommand, Course>();
		}
	}
}
