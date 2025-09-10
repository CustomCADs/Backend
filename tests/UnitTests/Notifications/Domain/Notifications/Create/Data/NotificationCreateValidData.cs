namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Data;

using static NotificationsData;

public class NotificationCreateValidData : NotificationCreateData
{
	public NotificationCreateValidData()
	{
		Add(MinValidType, MinValidDescription);
		Add(MaxValidType, MaxValidDescription);
	}
}
