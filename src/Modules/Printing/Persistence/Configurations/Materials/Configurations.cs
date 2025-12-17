using CustomCADs.Modules.Printing.Domain.Materials;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Printing.Persistence.Configurations.Materials;

public class Configurations : IEntityTypeConfiguration<Material>
{
	public void Configure(EntityTypeBuilder<Material> builder)
	{
		builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations()
			.SetSeeding();
	}
}
