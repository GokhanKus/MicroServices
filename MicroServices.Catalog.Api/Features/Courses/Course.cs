using MicroServices.Catalog.Api.Features.Categories;

namespace MicroServices.Catalog.Api.Features.Courses
{
	public class Course : BaseEntity
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public decimal Price { get; set; }
		public Guid UserId { get; set; } //ilerde key cloak kullanilacak
		public string? ImageUrl { get; set; }
		public DateTime Created { get; set; } //global app'lerde DateTimeOffSet yaparak +2 -3 gibi zaman dilimlerini de tutmaliyiz

		public Guid CategoryId { get; set; }
		public Category Category { get; set; } = default!;

		public Feature Feature { get; set; } = default!;
	}
}