using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Read;

public sealed class ReadNotificationHandler(INotificationReads reads, IUnitOfWork uow)
	: ICommandHandler<ReadNotificationCommand>
{
	public async Task Handle(ReadNotificationCommand req, CancellationToken ct = default)
	{
		Notification notification = await reads.SingleByIdAsync(req.Id, track: true, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Notification>.ById(req.Id);

		if (req.CallerId != notification.ReceiverId)
		{
			throw CustomAuthorizationException<Notification>.ById(req.Id);
		}

		notification.Read();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
