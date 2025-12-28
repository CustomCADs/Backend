using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Open;

public sealed class OpenNotificationHandler(
	INotificationReads reads,
	IUnitOfWork uow
) : ICommandHandler<OpenNotificationCommand>
{
	public async Task Handle(OpenNotificationCommand req, CancellationToken ct = default)
	{
		Notification notification = await reads.SingleByIdAsync(req.Id, track: true, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Notification>.ById(req.Id);

		if (req.CallerId != notification.ReceiverId)
		{
			throw CustomAuthorizationException<Notification>.ById(req.Id);
		}

		notification.Open();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
