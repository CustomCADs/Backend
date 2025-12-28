using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;

public sealed class LoginUserHandler(
	IUserService service,
	ITokenService tokenService
) : ICommandHandler<LoginUserCommand, TokensDto>
{
	private const string LoginError = "Username or Password incorrect.";

	public async Task<TokensDto> Handle(LoginUserCommand req, CancellationToken ct)
	{
		User user = await GetUserAsync(req.Username).ConfigureAwait(false);
		if (!user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom(
				message: $"User: {user.Username} hasn't verified their email."
			);
		}

		DateTimeOffset? lockoutEnd = await service.GetIsLockedOutAsync(user.Username).ConfigureAwait(false);
		if (lockoutEnd.HasValue)
		{
			TimeSpan timeLeft = lockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
			int seconds = Convert.ToInt32(timeLeft.TotalSeconds);

			throw CustomAuthorizationException<User>.Custom(
				message: $"The max attempts for logging into Account: {user.Username} has been reached. The account has been locked out for {seconds} seconds."
			);
		}

		if (!await service.CheckPasswordAsync(user.Username, req.Password).ConfigureAwait(false))
		{
			throw CustomAuthorizationException<User>.Custom(LoginError);
		}

		RefreshToken rt = tokenService.IssueRefreshToken(
			createRefreshToken: (token) => user.AddRefreshToken(token, longerSession: false)
		);
		await service.SaveRefreshTokensAsync(user).ConfigureAwait(false);

		return tokenService.IssueTokens(user, rt);
	}

	private async Task<User> GetUserAsync(string username)
	{
		try
		{
			return await service.GetByUsernameAsync(username).ConfigureAwait(false);
		}
		catch (Exception ex) when (ex.Message.EndsWith($"{username} does not exist."))
		{
			throw CustomAuthorizationException<User>.Custom(LoginError);
		}
	}

}
