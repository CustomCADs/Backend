using CustomCADs.Modules.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.ViewedProducts;

internal static class Utilities
{
	extension(EntityTypeBuilder<ViewedProduct> builder)
	{
		internal EntityTypeBuilder<ViewedProduct> SetPrimaryKey()
		{
			builder.HasKey(x => new { x.AccountId, x.ProductId });

			return builder;
		}

		internal EntityTypeBuilder<ViewedProduct> SetForeignKeys()
		{
			builder
				.HasOne(x => x.Account)
				.WithMany()
				.HasForeignKey(x => x.AccountId)
				.OnDelete(DeleteBehavior.Cascade);

			return builder;
		}

		internal EntityTypeBuilder<ViewedProduct> SetStronglyTypedIds()
		{
			builder.Property(x => x.AccountId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			builder.Property(x => x.ProductId)
				.HasConversion(
					x => x.Value,
					x => ProductId.New(x)
				);

			return builder;
		}
	}

}
