using CustomCADs.Modules.Catalog.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Catalog.Persistence.Configurations.ProductTags;

internal static class Utilities
{
	extension(EntityTypeBuilder<ProductTag> builder)
	{
		internal EntityTypeBuilder<ProductTag> SetPrimaryKey()
		{
			builder.HasKey(x => new { x.ProductId, x.TagId });

			return builder;
		}

		internal EntityTypeBuilder<ProductTag> SetForeignKeys()
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

		internal EntityTypeBuilder<ProductTag> SetStronglyTypedIds()
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

}
