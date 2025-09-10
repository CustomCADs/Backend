using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Notifications.Application.Notifications.Events;

public class NotificationRequestedHandler(IWrites<Notification> writes, IUnitOfWork uow)
{
	public async Task Handle(NotificationRequestedEvent req)
	{
		await writes.AddAsync(
			entity: Notification.Create(
				type: req.Type.ToString(),
				content: new(req.Description, req.Link),
				authorId: req.AuthorId,
				receiverId: req.ReceiverId
			)
		).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
