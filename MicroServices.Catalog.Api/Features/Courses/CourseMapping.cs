using MicroServices.Catalog.Api.Features.Courses.Create;
using MicroServices.Catalog.Api.Features.Courses.Dtos;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public class CourseMapping : Profile
	{
		public CourseMapping()
		{
			CreateMap<CreateCourseCommand, Course>();
			CreateMap<CourseDto, Course>().ReverseMap();
			CreateMap<FeatureDto, Feature>().ReverseMap();
		}
	}
}
