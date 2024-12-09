﻿using MicroServices.Catalog.Api.Features.Categories.Create;
using MicroServices.Catalog.Api.Features.Courses.Create;
using MicroServices.Catalog.Api.Features.Courses.GetAll;
using MicroServices.Catalog.Api.Features.Courses.GetById;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public static class CourseEndpointExt
	{
		public static void AddCourseGroupEndpointExt(this WebApplication app)
		{
			app.MapGroup("api/courses").WithTags("Courses")
				.CreateCourseGroupItemEndpoint()
				.GetAllCoursesGroupItemEndpoint()
				.GetByIdCourseGroupItemEndpoint();
		}
	}
}
