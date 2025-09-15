using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Accounts.Persistence.Configurations.ViewedProducts;

static class Utilities
{
	public static EntityTypeBuilder<ViewedProduct> SetPrimaryKey(this EntityTypeBuilder<ViewedProduct> builder)
	{
		builder.HasKey(x => new { x.AccountId, x.ProductId });

		return builder;
	}

	public static EntityTypeBuilder<ViewedProduct> SetForeignKeys(this EntityTypeBuilder<ViewedProduct> builder)
	{
		builder
			.HasOne(x => x.Account)
			.WithMany()
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		return builder;
	}

	public static EntityTypeBuilder<ViewedProduct> SetStronglyTypedIds(this EntityTypeBuilder<ViewedProduct> builder)
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
