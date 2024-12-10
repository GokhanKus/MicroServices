using MicroServices.Catalog.Api.Features.Categories;
using MicroServices.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;

namespace MicroServices.Catalog.Api.Repositories
{
	public static class SeedData
	{
		public static async Task AddSeedDataExt(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

			if (!context.Categories.Any())
			{
				var categories = new List<Category>
				{
					new Category{Id = NewId.NextSequentialGuid(),Name = "Development"},
					new Category{Id = NewId.NextSequentialGuid(),Name = "Business"},
					new Category{Id = NewId.NextSequentialGuid(),Name = "IT & Software"},
					new Category{Id = NewId.NextSequentialGuid(),Name = "Office Productivity"},
					new Category{Id = NewId.NextSequentialGuid(),Name = "Personal Development"},
				};
				context.Categories.AddRange(categories);
				await context.SaveChangesAsync();
			}

			if (!context.Courses.Any())
			{
				var category = await context.Categories.FirstAsync();

				var randomUserId = NewId.NextGuid();

				List<Course> courses =
				[
					new()
					{
						Id = NewId.NextSequentialGuid(),
						Name = "C#",
						Description = "C# Course",
						Price = 100,
						UserId = randomUserId,
						Created = DateTime.UtcNow, //global app yapilacaksa Now yerine UtcNow kullanilmali cunku zone bilgileri saat vs. utcnowda tutulur, daha ileri gidip offset de kullanilabilir
						Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
						CategoryId = category.Id
					},

					new()
					{
						Id = NewId.NextSequentialGuid(),
						Name = "Java",
						Description = "Java Course",
						Price = 200,
						UserId = randomUserId,
						Created = DateTime.UtcNow,
						Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
						CategoryId = category.Id
					},

					new()
					{
						Id = NewId.NextSequentialGuid(),
						Name = "Python",
						Description = "Python Course",
						Price = 300,
						UserId = randomUserId,
						Created = DateTime.UtcNow,
						Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
						CategoryId = category.Id
					}
				];
				context.Courses.AddRange(courses);
				await context.SaveChangesAsync();
			}
		}
	}
}
#region program.cs'te app.AddSeedDataExt().ContinueWith(x =>
/*
bu async metotda await kullanmiyoruz cunku ilk eklenen seed datalar cok fazla da olabilir ve program ilk acilirken bunun icin beklemesin, eger await kullansaydik bekleyecekti
arka planda işlem basarili ya da basarisiz bir sekilde tamamlansin ve continuewith metodu ile varsa hatalari yakalayip firlatsın yoksa console'a success mesaji basalim

 */
#endregion