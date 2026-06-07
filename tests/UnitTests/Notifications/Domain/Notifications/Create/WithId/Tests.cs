namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.WithId;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenNotificationIsValid(string name, string description)
	{
		CreateNotification(name, description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties(string type, string description)
	{
		var notification = CreateNotification(type, description, ValidLink, ValidAuthorId, ValidReceiverId, ValidId);

		Assert.Multiple(
			() => Assert.Equal(type, notification.Type),
			() => Assert.Equal(description, notification.Content.Description),
			() => Assert.Equal(ValidLink, notification.Content.Link),
			() => Assert.Equal(ValidAuthorId, notification.AuthorId),
			() => Assert.Equal(ValidReceiverId, notification.ReceiverId)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenNotificationIsInvalid(string type, string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(
			() => CreateNotification(type, description)
		);
	}
}
