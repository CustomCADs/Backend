namespace CustomCADs.UnitTests.Notifications.Application.Notifications;

using CustomCADs.Shared.Domain.TypedIds.Accounts;
using static NotificationsData;

public class NotificationsBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	public static Notification CreateNotification(
		NotificationId? id = null,
		string? type = null,
		string? description = null,
		string? link = null,
		AccountId? authorId = null,
		AccountId? receiverId = null
	) => Notification.Create(
			id: id ?? ValidId,
			type: type ?? MaxValidType,
			content: new(description ?? MaxValidDescription, link),
			authorId: authorId ?? ValidAuthorId,
			receiverId: receiverId ?? ValidReceiverId
		);
}
