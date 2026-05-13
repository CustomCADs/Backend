using CustomCADs.Modules.Accounts.Domain.Accounts.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.ViewedProducts;

public class Configurations : IEntityTypeConfiguration<ViewedProduct>
{
	public void Configure(EntityTypeBuilder<ViewedProduct> builder)
	{
		builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds();
	}
}

