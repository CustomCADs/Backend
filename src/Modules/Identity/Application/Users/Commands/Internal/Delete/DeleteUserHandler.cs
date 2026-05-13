using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;

public sealed class DeleteUserHandler(
	IUserService service,
	IEventRaiser raiser
) : ICommandHandler<DeleteUserCommand>
{
	public async Task Handle(DeleteUserCommand req, CancellationToken ct = default)
	{
		await service.DeleteAsync(req.CallerId).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserDeletedApplicationEvent(req.CallerId)
		).ConfigureAwait(false);
	}
}
