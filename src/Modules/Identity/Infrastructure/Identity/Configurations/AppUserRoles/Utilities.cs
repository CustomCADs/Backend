using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppUserRoles;

using static Infrastructure.Constants;

internal static class Utilities
{
	extension(EntityTypeBuilder<AppUserRole> builder)
	{
		internal EntityTypeBuilder<AppUserRole> SetSeeding()
		{
			builder.HasData([
				new() { RoleId = Roles.CustomerId, UserId = Users.CustomerId },
				new() { RoleId = Roles.ContributorId, UserId = Users.ContributorId },
				new() { RoleId = Roles.DesignerId, UserId = Users.DesignerId },
				new() { RoleId = Roles.AdminId, UserId = Users.AdminId },
		]);

			return builder;
		}
	}

}
