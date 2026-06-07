namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidTestData))]
	public void SetDescription_ShouldNotThrowException_WhenCustomValid(string description)
	{
		var notification = CreateNotification();
		notification.SetContent(notification.Content with { Description = description });
	}

	[Theory]
	[ClassData(typeof(ValidTestData))]
	public void SetDescription_ShouldPopulateProperties(string description)
	{
		var notification = CreateNotification();
		notification.SetContent(notification.Content with { Description = description });
		Assert.Equal(description, notification.Content.Description);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(
			() => CreateNotification().SetContent(new(description, ValidLink))
		);
	}
}
