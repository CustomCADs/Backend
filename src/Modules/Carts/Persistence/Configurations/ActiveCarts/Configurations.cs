using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Carts.Persistence.Configurations.ActiveCarts;

public class Configurations : IEntityTypeConfiguration<ActiveCartItem>
{
	public void Configure(EntityTypeBuilder<ActiveCartItem> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations();
}
