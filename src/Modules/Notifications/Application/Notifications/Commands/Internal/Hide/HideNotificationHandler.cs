using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Hide;

public class HideNotificationHandler(INotificationReads reads, IUnitOfWork uow)
	: ICommandHandler<HideNotificationCommand>
{
	public async Task Handle(HideNotificationCommand req, CancellationToken ct = default)
	{
		Notification notification = await reads.SingleByIdAsync(req.Id, track: true, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Notification>.ById(req.Id);

		if (req.CallerId != notification.ReceiverId)
		{
			throw CustomAuthorizationException<Notification>.ById(req.Id);
		}

		notification.Hide();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
