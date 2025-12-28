using CustomCADs.Modules.Notifications.Application.Contracts;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Events;

public class NotificationRequestedHandler(IWrites<Notification> writes, IUnitOfWork uow, IRequestSender sender, INotificationsRealTimeNotifier notifier)
{
	public async Task HandleAsync(NotificationRequestedEvent ae)
	{
		List<Notification> notifications = [];
		if (ae.ReceiverIds is [AccountId receiverId])
		{
			Notification notification = await writes.AddAsync(
				entity: Notification.Create(
					type: ae.Type.ToString(),
					content: new(ae.Description, ae.Link),
					authorId: ae.AuthorId,
					receiverId: receiverId
				)
			).ConfigureAwait(false);
			await uow.SaveChangesAsync().ConfigureAwait(false);

			notifications.Add(notification);
		}
		else notifications = [..
			await uow.InsertNotificationsAsync(
				notifications: Notification.CreateBulk(
					type: ae.Type.ToString(),
					content: new(ae.Description, ae.Link),
					authorId: ae.AuthorId,
					receiverIds: [.. ae.ReceiverIds.Distinct()]
				)
			).ConfigureAwait(false)
		];

		string author = await sender.SendQueryAsync(new GetUsernameByIdQuery(ae.AuthorId)).ConfigureAwait(false);
		await notifier.NotifyUsersAsync(
			ids: ae.ReceiverIds,
			message: "ReceiveNew",
			payload: notifications.Select(x => new
			{
				id = x.Id.Value,
				type = x.Type,
				description = x.Content.Description,
				link = x.Content.Link,
				status = x.Status,
				createdAt = x.CreatedAt,
				author = author,
			}).FirstOrDefault()
		).ConfigureAwait(false);
	}
}
