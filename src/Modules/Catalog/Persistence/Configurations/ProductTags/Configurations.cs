using CustomCADs.Modules.Catalog.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Catalog.Persistence.Configurations.ProductTags;

public class Configurations : IEntityTypeConfiguration<ProductTag>
{
	public void Configure(EntityTypeBuilder<ProductTag> builder)
	{
		builder
			.SetPrimaryKey()
			.SetForeignKeys()
			.SetStronglyTypedIds();
	}
}
