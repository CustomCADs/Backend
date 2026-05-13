using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Services;

public partial class AppUserService
{
	#region Lifecycle
	public async Task CreateAsync(User user, string password)
	{
		AppUser appUser = user.ToAppUser();
		await manager.CreateAsync(appUser, password).ConfigureAwait(false);
		await AddToRoleAsync(appUser, user.Role).ConfigureAwait(false);
	}

	public async Task CreateSSOAsync(User user, string provider)
	{
		AppUser appUser = user.ToAppUser(provider);
		await manager.CreateAsync(appUser).ConfigureAwait(false);
		await AddToRoleAsync(appUser, user.Role).ConfigureAwait(false);
	}

	private async Task AddToRoleAsync(AppUser user, string role)
	{
		IdentityResult result = await manager.AddToRoleAsync(user, role).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw new CustomException($"Couldn't create an account for: {user.Username}.");
		}
	}

	public async Task DeleteAsync(AccountId id)
	{
		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.AccountId == id).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(User.AccountId), id);

		await manager.DeleteAsync(appUser).ConfigureAwait(false);
	}
	#endregion

	#region Mutation
	public async Task<bool> CheckPasswordAsync(string username, string password)
	{
		AppUser appUser = await QueryByUsername(context.Users, username).FirstOrDefaultAsync().ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		bool success = await manager.CheckPasswordAsync(appUser, password).ConfigureAwait(false);
		if (success)
		{
			await manager.ResetAccessFailedCountAsync(appUser).ConfigureAwait(false);
		}
		else
		{
			await manager.AccessFailedAsync(appUser).ConfigureAwait(false);
		}

		return success;
	}

	public async Task UpdateUsernameAsync(UserId id, string username)
	{
		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id.Value).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(id), id);

		if (appUser.Username != username) appUser.Username = username;
		await manager.UpdateAsync(appUser).ConfigureAwait(false);
	}

	public async Task SaveRefreshTokensAsync(User user)
	{
		AppUser appUser = await context.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.Id == user.Id.Value)
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(user.Id), user.Id);

		appUser.FillRefreshTokens([.. user.RefreshTokens.Select(x => x.ToAppRefreshToken())]);
		await context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task RevokeRefreshTokenAsync(RefreshTokenId id)
	{
		(User User, RefreshToken RefreshToken) = await GetByRefreshTokenAsync(id).ConfigureAwait(false);
		User.RemoveRefreshToken(RefreshToken);

		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.Id == User.Id.Value).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(User.Id), User.Id);

		appUser.FillRefreshTokens([.. User.RefreshTokens.Select(x => x.ToAppRefreshToken())]);
		await context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task RevokeRefreshTokenAsync(string token)
	{
		(User User, RefreshToken RefreshToken) = await GetByRefreshTokenAsync(token).ConfigureAwait(false);
		User.RemoveRefreshToken(RefreshToken);

		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.Id == User.Id.Value).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(User.Id), User.Id);

		appUser.FillRefreshTokens([.. User.RefreshTokens.Select(x => x.ToAppRefreshToken())]);
		await context.SaveChangesAsync().ConfigureAwait(false);
	}
	#endregion

	#region Token Generation
	public async Task<string> GenerateEmailConfirmationTokenAsync(string username)
	{
		AppUser appUser = await QueryByUsername(context.Users, username).FirstOrDefaultAsync().ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		return await manager.GenerateEmailConfirmationTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task ConfirmEmailAsync(string username, string token)
	{
		AppUser appUser = await QueryByUsername(context.Users, username).FirstOrDefaultAsync().ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		IdentityResult result = await manager.ConfirmEmailAsync(appUser, token).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw CustomAuthorizationException<User>.Custom($"Error confirming Account: {username}'s email.");
		}
	}

	public async Task<string> GeneratePasswordResetTokenAsync(string email)
	{
		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(email), email);

		return await manager.GeneratePasswordResetTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task ResetPasswordAsync(string email, string token, string newPassword)
	{
		AppUser appUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(email), email);

		IdentityResult result = await manager.ResetPasswordAsync(appUser, token, newPassword).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw CustomAuthorizationException<User>.Custom($"Failed to reset Account: {email}'s password.");
		}
	}
	#endregion
}
