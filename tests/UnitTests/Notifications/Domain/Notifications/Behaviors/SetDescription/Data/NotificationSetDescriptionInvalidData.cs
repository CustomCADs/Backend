namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription.Data;

using static NotificationsData;

public class NotificationSetDescriptionInvalidData : NotificationSetDescriptionData
{
	public NotificationSetDescriptionInvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
