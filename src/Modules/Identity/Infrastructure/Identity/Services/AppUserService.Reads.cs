using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Services;

public partial class AppUserService
{
	private async Task<User> MapToUserAsync(AppUser user)
		=> user.ToUser(
			role: (await manager.GetRolesAsync(user).ConfigureAwait(false)).Single()
		);

	private static IQueryable<AppUser> QueryByUsername(IQueryable<AppUser> query, string username)
		=> query.Where(x => x.UserName == (x.IsSSO ? x.Provider + '/' + username : username));

	private async Task<(User User, RefreshToken RefreshToken)> GetByRefreshTokenAsync(RefreshTokenId refreshTokenId)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Id == refreshTokenId.Value))
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(RefreshToken.Id), refreshTokenId);

		return (
			User: await MapToUserAsync(appUser).ConfigureAwait(false),
			RefreshToken: appUser.RefreshTokens.First(x => x.Id == refreshTokenId.Value).ToRefreshToken()
		);
	}

	#region GetUserByX
	public async Task<User> GetByAccountIdAsync(AccountId accountId)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.AccountId == accountId)
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(User.AccountId), accountId);

		return await MapToUserAsync(appUser).ConfigureAwait(false);
	}

	public async Task<User> GetByUsernameAsync(string username)
	{
		AppUser appUser = await QueryByUsername(context.Users.Include(x => x.RefreshTokens), username)
			.FirstOrDefaultAsync()
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

	#region GetX
	public async Task<bool> GetExistsByUsernameAsync(string username)
		=> await QueryByUsername(context.Users, username).AnyAsync().ConfigureAwait(false);

	public async Task<bool> GetExistsByEmailAsync(string email)
		=> await context.Users
			.AnyAsync(x => x.Email == email)
			.ConfigureAwait(false);

	public async Task<bool> GetIsSSOByEmailAsync(string email)
		=> await context.Users
			.AnyAsync(x => x.Email == email && x.IsSSO)
			.ConfigureAwait(false);

	public async Task<DateTimeOffset?> GetIsLockedOutAsync(string username)
	{
		AppUser appUser = await QueryByUsername(context.Users, username)
			.FirstOrDefaultAsync()
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
