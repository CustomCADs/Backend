using CustomCADs.Modules.Carts.Domain.PurchasedCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Carts.Persistence.Configurations.PurchasedCarts;

internal static class Utilities
{
	extension(EntityTypeBuilder<PurchasedCart> builder)
	{
		internal EntityTypeBuilder<PurchasedCart> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<PurchasedCart> SetForeignKeys()
		{
			builder
				.HasMany(x => x.Items)
				.WithOne(x => x.Cart)
				.HasForeignKey(x => x.CartId)
				.OnDelete(DeleteBehavior.Cascade);

			return builder;
		}

		internal EntityTypeBuilder<PurchasedCart> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => PurchasedCartId.New(x)
				);

			builder.Property(x => x.BuyerId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			builder.Property(x => x.ShipmentId)
				.HasConversion(
					x => ShipmentId.Unwrap(x),
					x => ShipmentId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<PurchasedCart> SetValidations()
		{
			builder.Property(x => x.PurchasedAt)
				.IsRequired()
				.HasColumnName(nameof(PurchasedCart.PurchasedAt));

			builder.Property(x => x.PaymentStatus)
				.IsRequired()
				.HasColumnName(nameof(PurchasedCart.PaymentStatus))
				.HasConversion(
					x => x.ToString(),
					x => Enum.Parse<PaymentStatus>(x)
				);

			builder.Property(x => x.BuyerId)
				.IsRequired()
				.HasColumnName(nameof(PurchasedCart.BuyerId));

			builder.Property(x => x.ShipmentId)
				.HasColumnName(nameof(PurchasedCart.ShipmentId));

			return builder;
		}
	}
}
