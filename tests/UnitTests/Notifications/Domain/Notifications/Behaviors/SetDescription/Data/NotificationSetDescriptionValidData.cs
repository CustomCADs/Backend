namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription.Data;

using static NotificationsData;

public class NotificationSetDescriptionValidData : NotificationSetDescriptionData
{
	public NotificationSetDescriptionValidData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
