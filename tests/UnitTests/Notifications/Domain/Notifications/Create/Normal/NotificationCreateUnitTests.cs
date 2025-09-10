namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Normal;

using Data;
using static NotificationsData;

public class NotificationCreateUnitTests : NotificationsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(NotificationCreateValidData))]
	public void Create_ShouldNotThrowException_WhenNotificationIsValid(string name, string description)
	{
		CreateNotification(name, description, ValidLink, ValidAuthorId, ValidReceiverId);
	}

	[Theory]
	[ClassData(typeof(NotificationCreateValidData))]
	public void Create_ShouldPopulateProperties(string type, string description)
	{
		var notification = CreateNotification(type, description, ValidLink, ValidAuthorId, ValidReceiverId);

		Assert.Multiple(
			() => Assert.Equal(type, notification.Type),
			() => Assert.Equal(description, notification.Content.Description),
			() => Assert.Equal(ValidLink, notification.Content.Link),
			() => Assert.Equal(ValidAuthorId, notification.AuthorId),
			() => Assert.Equal(ValidReceiverId, notification.ReceiverId)
		);
	}

	[Theory]
	[ClassData(typeof(NotificationCreateInvalidNameData))]
	[ClassData(typeof(NotificationCreateInvalidDescriptionData))]
	public void Create_ShouldThrowException_WhenNotificationIsInvalid(string type, string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(
			() => CreateNotification(type, description, ValidLink, ValidAuthorId, ValidReceiverId)
		);
	}
}
