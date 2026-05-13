using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

public sealed class ChangeUsernameHandler(
	IUserService service,
	IEventRaiser raiser
) : ICommandHandler<ChangeUsernameCommand>
{
	public async Task Handle(ChangeUsernameCommand req, CancellationToken ct)
	{
		User user = await service.GetByAccountIdAsync(req.Id).ConfigureAwait(false);

		user.SetUsername(req.Username);
		await service.UpdateUsernameAsync(user.Id, user.Username).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserEditedApplicationEvent(
				Id: user.AccountId,
				Username: user.Username,
				Names: new(req.FirstName, req.LastName)
			)
		).ConfigureAwait(false);
	}
}
