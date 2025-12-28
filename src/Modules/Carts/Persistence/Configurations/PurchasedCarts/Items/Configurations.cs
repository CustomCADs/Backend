using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Carts.Persistence.Configurations.PurchasedCarts.Items;

public class Configurations : IEntityTypeConfiguration<PurchasedCartItem>
{
	public void Configure(EntityTypeBuilder<PurchasedCartItem> builder)
		=> builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds()
			.SetValidations();
}
