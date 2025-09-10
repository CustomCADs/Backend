using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications;

using static NotificationsData;

public class NotificationsBaseUnitTests
{
	public static Notification CreateNotification(
		string? type = null,
		string? description = null,
		string? link = null,
		AccountId? authorId = null,
		AccountId? receiverId = null
	) => Notification.Create(
			type: type ?? MaxValidType,
			content: new(description ?? MaxValidDescription, link),
			authorId: authorId ?? ValidAuthorId,
			receiverId: receiverId ?? ValidReceiverId
		);

	public static Notification CreateNotificationWithId(
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
