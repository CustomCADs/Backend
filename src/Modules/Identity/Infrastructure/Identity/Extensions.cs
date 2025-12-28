using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity;

public static class Extensions
{
	extension(AppUser appUser)
	{
		public async Task<User> ToUserWithRoleAsync(UserManager<AppUser> manager)
			=> appUser.ToUser(role: await manager.GetRoleAsync(appUser).ConfigureAwait(false));
	}

	extension(UserManager<AppUser> manager)
	{
		internal async Task<string> GetRoleAsync(AppUser appUser)
		{
			var roles = await manager.GetRolesAsync(appUser).ConfigureAwait(false);
			return roles.Single();
		}
	}

}
