using MicroServices.Catalog.Api.Features.Categories;
using MicroServices.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace MicroServices.Catalog.Api.Repositories
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Course> Courses { get; set; } = default!;
		public DbSet<Category> Categories { get; set; } = default!;
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public static AppDbContext Create(IMongoDatabase database)
		{
			var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

			return new AppDbContext(dbContextOptions.Options);
		}
	}
}
