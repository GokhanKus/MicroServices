using MicroServices.Catalog.Api.Features.Categories;
using MicroServices.Catalog.Api.Repositories;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public class Course : BaseEntity
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public decimal Price { get; set; }
		public Guid UserId { get; set; } //ilerde key cloak kullanilacak
		public string? Picture { get; set; }
		public DateTime CreatedDate{ get; set; } //global app'lerde DateTimeOffSet yaparak +2 -3 gibi zaman dilimlerini de tutmaliyiz

		public Guid	CategoryId{ get; set; }
		public Category Category { get; set; } = default!;
	}
}