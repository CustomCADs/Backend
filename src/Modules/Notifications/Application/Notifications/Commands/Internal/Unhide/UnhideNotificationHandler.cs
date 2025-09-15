using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Unhide;

public sealed class UnhideNotificationHandler(
	INotificationReads reads,
	IUnitOfWork uow
) : ICommandHandler<UnhideNotificationCommand>
{
	public async Task Handle(UnhideNotificationCommand req, CancellationToken ct = default)
	{
		Notification notification = await reads.SingleByIdAsync(req.Id, track: true, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Notification>.ById(req.Id);

		notification.Unhide();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
