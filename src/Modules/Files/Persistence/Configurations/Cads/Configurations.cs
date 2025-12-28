using CustomCADs.Modules.Files.Domain.Cads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Files.Persistence.Configurations.Cads;

public class Configurations : IEntityTypeConfiguration<Cad>
{
	public void Configure(EntityTypeBuilder<Cad> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValueObjects()
			.SetValidaitons();
}
