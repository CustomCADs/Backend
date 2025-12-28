using CustomCADs.Modules.Catalog.Domain.Tags;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Catalog.Persistence.Configurations.Tags;

public class Configurations : IEntityTypeConfiguration<Tag>
{
	public void Configure(EntityTypeBuilder<Tag> builder)
	{
		builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations()
			.SetSeeding();
	}
}
