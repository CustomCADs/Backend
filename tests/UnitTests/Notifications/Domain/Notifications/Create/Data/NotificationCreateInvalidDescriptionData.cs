namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Data;

using static NotificationsData;

public class NotificationCreateInvalidDescriptionData : NotificationCreateData
{
	public NotificationCreateInvalidDescriptionData()
	{
		Add(MinValidType, MinInvalidDescription);
		Add(MaxValidType, MaxInvalidDescription);
	}
}
