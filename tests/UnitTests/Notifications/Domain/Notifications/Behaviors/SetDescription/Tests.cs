
namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidTestData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldNotThrowException_WhenCustomValid(string description)
	{
		var notification = CreateNotification();
		notification.SetContent(notification.Content with { Description = description });
	}

	[Test]
	[MethodDataSource(typeof(ValidTestData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetDescription_ShouldPopulateProperties(string description)
	{
		var notification = CreateNotification();
		notification.SetContent(notification.Content with { Description = description });
		await Assert.That(notification.Content.Description).IsEqualTo(description);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(() => CreateNotification().SetContent(new(description, ValidLink)));
	}
}
