using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppUserRoles;

public class Configurations : IEntityTypeConfiguration<AppUserRole>
{
	public void Configure(EntityTypeBuilder<AppUserRole> builder)
		=> builder
			.SetSeeding();
}
