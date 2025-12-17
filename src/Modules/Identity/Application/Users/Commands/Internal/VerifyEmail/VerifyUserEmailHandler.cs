using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerifyEmail;

public sealed class VerifyUserEmailHandler(
	IUserService service,
	ITokenService tokenService
) : ICommandHandler<VerifyUserEmailCommand, TokensDto>
{
	public async Task<TokensDto> Handle(VerifyUserEmailCommand req, CancellationToken ct)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		if (user.Email.IsVerified)
		{
			throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} has already confirmed their email.");
		}
		await service.ConfirmEmailAsync(req.Username, req.Token).ConfigureAwait(false);

		RefreshToken rt = tokenService.IssueRefreshToken(
			createRefreshToken: (token) => user.AddRefreshToken(token, longerSession: false)
		);
		await service.SaveRefreshTokensAsync(user).ConfigureAwait(false);

		return tokenService.IssueTokens(user, rt);
	}
}
