using MicroServices.Catalog.Api.Features.Courses;
using MicroServices.Catalog.Api.Repositories;

namespace MicroServices.Catalog.Api.Features.Categories
{
	public class Category : BaseEntity
	{
		public string Name { get; set; } = default!;
		public List<Course>? Courses { get; set; }
	}
}
