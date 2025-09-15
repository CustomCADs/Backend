using CustomCADs.Catalog.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Catalog.Persistence.Configurations.ProductTags;

static class Utilities
{
	public static EntityTypeBuilder<ProductTag> SetPrimaryKey(this EntityTypeBuilder<ProductTag> builder)
	{
		builder.HasKey(x => new { x.ProductId, x.TagId });

		return builder;
	}

	public static EntityTypeBuilder<ProductTag> SetForeignKeys(this EntityTypeBuilder<ProductTag> builder)
	{
		builder
			.HasOne(x => x.Product)
			.WithMany()
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasOne(x => x.Tag)
			.WithMany()
			.HasForeignKey(x => x.TagId)
			.OnDelete(DeleteBehavior.Cascade);

		return builder;
	}

	public static EntityTypeBuilder<ProductTag> SetStronglyTypedIds(this EntityTypeBuilder<ProductTag> builder)
	{
		builder.Property(pt => pt.ProductId)
			.HasConversion(
				x => x.Value,
				x => ProductId.New(x)
			);

		builder.Property(pt => pt.TagId)
			.HasConversion(
				x => x.Value,
				x => TagId.New(x)
			);

		return builder;
	}
}
