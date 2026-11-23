using CustomCADs.Modules.Notifications.Application.Contracts;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Events;

public class NotificationRequestedHandler(
	IWrites<Notification> writes,
	IUnitOfWork uow, IRequestSender sender,
	INotificationsRealTimeNotifier notifier
) : IEventHandler<NotificationRequestedEvent>
{
	public async Task HandleAsync(NotificationRequestedEvent @event)
	{
		if (@event.ReceiverIds.Length == 0) return;
		string author = await sender.SendQueryAsync(new GetUsernameByIdQuery(@event.AuthorId)).ConfigureAwait(false);

		List<Notification> notifications = @event.ReceiverIds.Length == 1
			? [await CreateNotificationAsync(@event).ConfigureAwait(false)]
			: await BulkCreateNotificationsAsync(@event).ConfigureAwait(false);

		foreach (Notification notification in notifications)
		{
			await notifier.NotifyUserAsync(
				id: notification.ReceiverId,
				message: "ReceiveNew",
				payload: new
				{
					id = notification.Id.Value,
					status = notification.Status,
					createdAt = notification.CreatedAt,
					type = @event.Type,
					description = @event.Description,
					link = @event.Link,
					author = author,
				}
			).ConfigureAwait(false);
		}
	}

	private async Task<Notification> CreateNotificationAsync(NotificationRequestedEvent @event)
	{
		Notification notification = await writes.AddAsync(
			entity: Notification.Create(
				type: @event.Type.ToString(),
				content: new(@event.Description, @event.Link),
				authorId: @event.AuthorId,
				receiverId: @event.ReceiverIds.First()
			)
		).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		return notification;
	}

	private async Task<List<Notification>> BulkCreateNotificationsAsync(NotificationRequestedEvent @event)
	{
		ICollection<Notification> notifications = await uow.InsertNotificationsAsync(
			notifications: Notification.CreateBulk(
				type: @event.Type.ToString(),
				content: new(@event.Description, @event.Link),
				authorId: @event.AuthorId,
				receiverIds: [.. @event.ReceiverIds.Distinct()]
			)
		).ConfigureAwait(false);

		return [.. notifications];
	}
}
