using CustomCADs.Modules.Identity.Application.Extensions;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Refresh;

public sealed class RefreshUserHandler(
	IUserService service,
	ITokenService tokenService
) : ICommandHandler<RefreshUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(RefreshUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.Token))
		{
			throw CustomAuthorizationException<User>.NoRefreshToken();
		}

		(User User, RefreshToken RefreshToken) = await service.GetByRefreshTokenAsync(req.Token).ConfigureAwait(false);
		if (RefreshToken.ExpiresAt < DateTime.UtcNow)
		{
			throw CustomAuthorizationException<User>.RefreshTokenExpired();
		}

		return tokenService.IssueTokens(User, RefreshToken);
	}
}
