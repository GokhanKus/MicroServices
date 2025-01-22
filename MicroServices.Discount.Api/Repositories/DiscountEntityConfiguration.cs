using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroServices.Discount.Api.Repositories
{
	public class DiscountEntityConfiguration : IEntityTypeConfiguration<Discount>
	{
		public void Configure(EntityTypeBuilder<Discount> builder)
		{
			builder.ToCollection("discounts");
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.Id).HasElementName("_id");
			builder.Property(x => x.Id).ValueGeneratedNever();
			builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10); //fieldlar(column) kucuk harf olacak nosql mongodb icin
			builder.Property(x => x.Rate).HasElementName("rate");
			builder.Property(x => x.UserId).HasElementName("user_id").HasMaxLength(10);
			builder.Property(x => x.Created).HasElementName("created");
			builder.Property(x => x.Updated).HasElementName("updated");
			builder.Property(x => x.Expired).HasElementName("expired").HasMaxLength(200);
		}
	}
}
