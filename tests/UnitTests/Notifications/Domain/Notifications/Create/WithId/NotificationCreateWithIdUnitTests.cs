namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.WithId;

using Data;
using static NotificationsData;

public class NotificationCreateWithIdWithIdUnitTests : NotificationsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(NotificationCreateValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenNotificationIsValid(string name, string description)
	{
		CreateNotificationWithId(ValidId, name, description, ValidLink, ValidAuthorId, ValidReceiverId);
	}

	[Theory]
	[ClassData(typeof(NotificationCreateValidData))]
	public void CreateWithId_ShouldPopulateProperties(string type, string description)
	{
		var notification = CreateNotificationWithId(ValidId, type, description, ValidLink, ValidAuthorId, ValidReceiverId);

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
	public void CreateWithId_ShouldThrowException_WhenNotificationIsInvalid(string type, string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(
			() => CreateNotificationWithId(ValidId, type, description, ValidLink, ValidAuthorId, ValidReceiverId)
		);
	}
}
