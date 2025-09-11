using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Events;

public class NotificationRequestedHandler(IWrites<Notification> writes, IUnitOfWork uow)
{
	public async Task Handle(NotificationRequestedEvent req)
	{
		if (req.ReceiverIds is [AccountId receiverId])
		{
			await writes.AddAsync(
				entity: Notification.Create(
					type: req.Type.ToString(),
					content: new(req.Description, req.Link),
					authorId: req.AuthorId,
					receiverId: receiverId
				)
			).ConfigureAwait(false);
			await uow.SaveChangesAsync().ConfigureAwait(false);
			return;
		}

		await uow.InsertNotificationsAsync(
			notifications: Notification.CreateBulk(
				type: req.Type.ToString(),
				content: new(req.Description, req.Link),
				authorId: req.AuthorId,
				receiverIds: req.ReceiverIds
			)
		).ConfigureAwait(false);
	}
}
