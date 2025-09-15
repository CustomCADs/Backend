using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Carts.Persistence.Configurations.PurchasedCarts;

public static class Utilities
{
	public static EntityTypeBuilder<PurchasedCart> SetPrimaryKey(this EntityTypeBuilder<PurchasedCart> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<PurchasedCart> SetForeignKeys(this EntityTypeBuilder<PurchasedCart> builder)
	{
		builder
			.HasMany(x => x.Items)
			.WithOne(x => x.Cart)
			.HasForeignKey(x => x.CartId)
			.OnDelete(DeleteBehavior.Cascade);

		return builder;
	}

	public static EntityTypeBuilder<PurchasedCart> SetStronglyTypedIds(this EntityTypeBuilder<PurchasedCart> builder)
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

	public static EntityTypeBuilder<PurchasedCart> SetValidations(this EntityTypeBuilder<PurchasedCart> builder)
	{
		builder.Property(x => x.PurchasedAt)
			.IsRequired()
			.HasColumnName(nameof(PurchasedCart.PurchasedAt));

		builder.Property(x => x.PaymentStatus)
			.IsRequired()
			.HasConversion(
				x => x.ToString(),
				x => Enum.Parse<PaymentStatus>(x)
			).HasColumnName(nameof(PurchasedCart.PaymentStatus));

		builder.Property(x => x.BuyerId)
			.IsRequired()
			.HasColumnName(nameof(PurchasedCart.BuyerId));

		builder.Property(x => x.ShipmentId)
			.HasColumnName(nameof(PurchasedCart.ShipmentId));

		return builder;
	}
}
