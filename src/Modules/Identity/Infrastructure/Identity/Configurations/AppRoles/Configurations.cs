using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppRoles;

public class Configurations : IEntityTypeConfiguration<AppRole>
{
	public void Configure(EntityTypeBuilder<AppRole> builder)
		=> builder
			.SetSeeding();
}
