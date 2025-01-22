using MongoDB.Driver;
using System.Reflection;

namespace MicroServices.Discount.Api.Repositories
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Discount> Discounts { get; set; } = null!;
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public static AppDbContext Create(IMongoDatabase database)
		{
			var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
				.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

			return new AppDbContext(dbContextOptions.Options);
		}
	}
}
