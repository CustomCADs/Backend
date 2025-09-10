namespace CustomCADs.Notifications.Domain.Notifications.ValueObjects;

public record NotificationContent(
	string Description,
	string? Link = null
)
{
	public NotificationContent() : this(string.Empty) { }
}
