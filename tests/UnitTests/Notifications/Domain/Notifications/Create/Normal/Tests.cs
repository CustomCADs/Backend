
namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create.Normal;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenNotificationIsValid(string name, string description)
	{
		Notification.Create(name, new(description, ValidLink), ValidAuthorId, ValidReceiverId);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties(string type, string description)
	{
		var notification = Notification.Create(type, new(description, ValidLink), ValidAuthorId, ValidReceiverId);

		using (Assert.Multiple())
		{
			await Assert.That(notification.Type).IsEqualTo(type);
			await Assert.That(notification.Content.Description).IsEqualTo(description);
			await Assert.That(notification.Content.Link).IsEqualTo(ValidLink);
			await Assert.That(notification.AuthorId).IsEqualTo(ValidAuthorId);
			await Assert.That(notification.ReceiverId).IsEqualTo(ValidReceiverId);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenNotificationIsInvalid(string type, string description)
	{
		Assert.Throws<CustomValidationException<Notification>>(() => Notification.Create(type, new(description, ValidLink), ValidAuthorId, ValidReceiverId));
	}
}
