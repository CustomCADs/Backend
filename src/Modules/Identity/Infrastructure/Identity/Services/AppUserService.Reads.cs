using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Services;

public partial class AppUserService
{
	private async Task<User> MapToUserAsync(AppUser user)
		=> user.ToUser(
			role: (await manager.GetRolesAsync(user).ConfigureAwait(false)).Single()
		);

	#region GetUserByX
	public async Task<User> GetByUsernameAsync(string username)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.UserName == (x.IsSSO ? x.Provider + '/' + username : username))
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		return await MapToUserAsync(appUser).ConfigureAwait(false);
	}

	public async Task<User> GetByEmailAsync(string email)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.Email == email)
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(email), email);

		return await MapToUserAsync(appUser).ConfigureAwait(false);
	}

	public async Task<(User User, RefreshToken RefreshToken)> GetByRefreshTokenAsync(string token)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Value == token))
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(token), token);

		return (
			User: await MapToUserAsync(appUser).ConfigureAwait(false),
			RefreshToken: appUser.RefreshTokens.First(x => x.Value == token).ToRefreshToken()
		);
	}
	#endregion

	#region GetPropertyX
	public async Task<bool> GetExistsByUsernameAsync(string username)
		=> await context.Users
			.AnyAsync(x => x.UserName == (x.IsSSO ? x.Provider + '/' + username : username))
			.ConfigureAwait(false);

	public async Task<bool> GetExistsByEmailAsync(string email)
		=> await context.Users
			.AnyAsync(x => x.Email == email)
			.ConfigureAwait(false);

	public async Task<bool> GetIsSSOByEmailAsync(string email)
		=> await context.Users
			.AnyAsync(x => x.Email == email && x.IsSSO)
			.ConfigureAwait(false);

	public async Task<AccountId> GetAccountIdAsync(string username)
	{
		AccountId accountId = await context.Users
			.Where(x => x.UserName == (x.IsSSO ? x.Provider + '/' + username : username))
			.Select(x => x.AccountId)
			.FirstOrDefaultAsync()
			.ConfigureAwait(false);

		if (accountId.IsEmpty())
		{
			throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);
		}

		return accountId;
	}

	public async Task<DateTimeOffset?> GetIsLockedOutAsync(string username)
	{
		AppUser appUser = await context.Users
			.FirstOrDefaultAsync(x => x.UserName == (x.IsSSO ? x.Provider + '/' + username : username))
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		bool isLockedOut = await manager.IsLockedOutAsync(appUser).ConfigureAwait(false);
		if (!isLockedOut)
		{
			return null;
		}

		return appUser.LockoutEnd;
	}
	#endregion
}
