using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Contracts;

public interface IUserService
{
	#region GetUserByX
	Task<User> GetByUsernameAsync(string username);
	Task<User> GetByEmailAsync(string email);
	Task<(User User, RefreshToken RefreshToken)> GetByRefreshTokenAsync(string token);
	#endregion

	#region GetX
	Task<bool> GetExistsByUsernameAsync(string username);
	Task<bool> GetExistsByEmailAsync(string email);
	Task<AccountId> GetAccountIdAsync(string username);
	Task<DateTimeOffset?> GetIsLockedOutAsync(string username);
	#endregion

	#region Lifecycle
	Task CreateAsync(User user, string password);
	Task CreateSSOAsync(User user, string provider);
	Task DeleteAsync(string username);
	#endregion

	#region Mutation
	Task<bool> CheckPasswordAsync(string username, string password);
	Task UpdateUsernameAsync(UserId id, string username);
	Task SaveRefreshTokensAsync(User user);
	Task RevokeRefreshTokenAsync(string token);
	#endregion

	#region Token Generation
	Task<string> GenerateEmailConfirmationTokenAsync(string username);
	Task ConfirmEmailAsync(string username, string token);
	Task<string> GeneratePasswordResetTokenAsync(string email);
	Task ResetPasswordAsync(string email, string token, string newPassword);
	#endregion
}
