using CustomCADs.Modules.Carts.Domain.PurchasedCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Carts.Persistence.Configurations.PurchasedCarts;

public class Configurations : IEntityTypeConfiguration<PurchasedCart>
{
	public void Configure(EntityTypeBuilder<PurchasedCart> builder)
		=> builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds()
			.SetValidations();
}
