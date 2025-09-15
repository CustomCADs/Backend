using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public sealed class RefreshUserHandler(
	IUserService service,
	ITokenService tokenService
) : ICommandHandler<RefreshUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(RefreshUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.Token))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}

		(User User, RefreshToken RefreshToken) = await service.GetByRefreshTokenAsync(req.Token).ConfigureAwait(false);
		if (RefreshToken.ExpiresAt < DateTime.UtcNow)
		{
			throw CustomAuthorizationException<User>.Custom("Refresh Token found, but expired.");
		}

		RefreshToken rt = tokenService.IssueRefreshToken(
			createRefreshToken: (token) => User.AddRefreshToken(token, longerSession: false)
		);
		await service.SaveRefreshTokensAsync(User).ConfigureAwait(false);

		return tokenService.IssueTokens(User, rt);
	}
}
