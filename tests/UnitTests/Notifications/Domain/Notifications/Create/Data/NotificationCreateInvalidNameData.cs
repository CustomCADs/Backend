namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Data;

using static NotificationsData;

public class NotificationCreateInvalidNameData : NotificationCreateData
{
	public NotificationCreateInvalidNameData()
	{
		Add(MinInvalidType, MinValidDescription);
		Add(MaxInvalidType, MaxValidDescription);
	}
}
