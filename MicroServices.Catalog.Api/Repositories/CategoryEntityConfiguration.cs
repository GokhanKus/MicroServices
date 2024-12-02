using MicroServices.Catalog.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroServices.Catalog.Api.Repositories
{
	public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToCollection("categories");//mongodb'de table = collection ve kucuk harf olur 
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedNever();//snow flake alg ile id biz uretecegiz
			builder.Ignore(x => x.Courses);//mongodb no sql db'mizde category'e ait boyle bir field olmayacak
		}
	}
}
