using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Logout;

public sealed class LogoutUserHandler(
	IUserService service
) : ICommandHandler<LogoutUserCommand>
{
	public async Task Handle(LogoutUserCommand req, CancellationToken ct)
	{
		if (string.IsNullOrEmpty(req.RefreshToken))
		{
			throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");
		}
		await service.RevokeRefreshTokenAsync(req.RefreshToken).ConfigureAwait(false);
	}
}
