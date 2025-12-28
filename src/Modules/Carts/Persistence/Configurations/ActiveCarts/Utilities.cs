using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Carts.Persistence.Configurations.ActiveCarts;

internal static class Utilities
{
	extension(EntityTypeBuilder<ActiveCartItem> builder)
	{
		internal EntityTypeBuilder<ActiveCartItem> SetPrimaryKey()
		{
			builder.HasKey(x => new { x.ProductId, x.BuyerId });

			return builder;
		}

		internal EntityTypeBuilder<ActiveCartItem> SetStronglyTypedIds()
		{
			builder.Property(x => x.BuyerId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			builder.Property(x => x.ProductId)
				.HasConversion(
					x => x.Value,
					x => ProductId.New(x)
				);

			builder.Property(x => x.CustomizationId)
				.HasConversion(
					x => CustomizationId.Unwrap(x),
					x => CustomizationId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<ActiveCartItem> SetValidations()
		{
			builder.Property(x => x.BuyerId)
				.IsRequired()
				.HasColumnName(nameof(ActiveCartItem.BuyerId));

			builder.Property(x => x.Quantity)
				.IsRequired()
				.HasColumnName(nameof(ActiveCartItem.Quantity));

			builder.Property(x => x.ForDelivery)
				.IsRequired()
				.HasColumnName(nameof(ActiveCartItem.ForDelivery));

			builder.Property(x => x.AddedAt)
				.IsRequired()
				.HasColumnName(nameof(ActiveCartItem.AddedAt));

			builder.Property(x => x.ProductId)
				.IsRequired()
				.HasColumnName(nameof(ActiveCartItem.ProductId));

			builder.Property(x => x.CustomizationId)
				.IsRequired(false)
				.HasColumnName(nameof(ActiveCartItem.CustomizationId));

			return builder;
		}
	}

}
