using CustomCADs.Modules.Identity.Application.Extensions;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete.Fingerprints;

public class DeleteFingerprintHandler(IUserService service) : ICommandHandler<DeleteFingerprintCommand>
{
	public async Task Handle(DeleteFingerprintCommand req, CancellationToken ct = default)
	{
		if (string.IsNullOrEmpty(req.CurrentRefreshToken))
		{
			throw CustomAuthorizationException<User>.NoRefreshToken();
		}

		(User user, RefreshToken refreshToken) = await service.GetByRefreshTokenAsync(req.CurrentRefreshToken).ConfigureAwait(false);
		if (req.CallerId != user.AccountId)
		{
			throw CustomAuthorizationException<User>.ById(user.AccountId);
		}

		if (refreshToken.Id == req.RefreshTokenId)
		{
			throw new CustomException("Cannot delete User's current RefreshToken");
		}
		await service.RevokeRefreshTokenAsync(req.RefreshTokenId).ConfigureAwait(false);
	}
}
