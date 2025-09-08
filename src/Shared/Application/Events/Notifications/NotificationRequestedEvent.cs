using CustomCADs.Shared.Application.Dtos.Notifications;

namespace CustomCADs.Shared.Application.Events.Notifications;

public record NotificationRequestedEvent(
	NotificationType Type,
	string Description,
	string? Link,
	AccountId AuthorId,
	AccountId ReceiverId
) : BaseApplicationEvent;
