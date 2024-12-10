using MicroServices.Catalog.Api.Features.Courses.Create;
using MicroServices.Catalog.Api.Features.Courses.Dtos;
using MicroServices.Catalog.Api.Features.Courses.Update;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public class CourseMapping : Profile
	{
		public CourseMapping()
		{
			CreateMap<CreateCourseCommand, Course>();
			CreateMap<UpdateCourseCommand, Course>();

			CreateMap<CourseDto, Course>().ReverseMap();
			CreateMap<FeatureDto, Feature>().ReverseMap();
		}
	}
}
