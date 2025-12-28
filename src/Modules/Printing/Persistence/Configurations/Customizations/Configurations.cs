using CustomCADs.Modules.Printing.Domain.Customizations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Printing.Persistence.Configurations.Customizations;

public class Configurations : IEntityTypeConfiguration<Customization>
{
	public void Configure(EntityTypeBuilder<Customization> builder)
	{
		builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations();
	}
}
