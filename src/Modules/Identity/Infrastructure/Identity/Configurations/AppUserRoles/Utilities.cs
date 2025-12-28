using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppUserRoles;

using static DomainConstants;
using AppUserRole = Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>;

internal static class Utilities
{
	extension(EntityTypeBuilder<AppUserRole> builder)
	{
		internal EntityTypeBuilder<AppUserRole> SetSeeding()
		{
			builder.HasData([
				new() { RoleId = new(Roles.CustomerId), UserId = new(Users.CustomerUserId) },
				new() { RoleId = new(Roles.ContributorId), UserId = new(Users.ContributorUserId) },
				new() { RoleId = new(Roles.DesignerId), UserId = new(Users.DesignerUserId) },
				new() { RoleId = new(Roles.AdminId), UserId = new(Users.AdminUserId) },
		]);

			return builder;
		}
	}

}
