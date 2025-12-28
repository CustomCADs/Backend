using CustomCADs.Modules.Files.Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Files.Persistence.Configurations.Images;

public class Configurations : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidaitons()
			.SetSeeding();
}
