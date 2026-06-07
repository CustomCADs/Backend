namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Normal;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenNotificationIsValid(string name, string description)
	{
		Notification.Create(name, new(description, ValidLink), ValidAuthorId, ValidReceiverId);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties(string type, string description)
	{
		var notification = Notification.Create(type, new(description, ValidLink), ValidAuthorId, ValidReceiverId);

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
			() => Notification.Create(type, new(description, ValidLink), ValidAuthorId, ValidReceiverId)
		);
	}
}
