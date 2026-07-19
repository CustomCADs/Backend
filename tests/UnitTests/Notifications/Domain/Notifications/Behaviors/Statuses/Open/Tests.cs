using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Open;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	public async Task Open_ShouldSucceed_WhenRead()
	{
		Notification notification = CreateNotification();
		notification.Read();

		notification.Open();

		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Opened);
	}

	[Test]
	public void Open_ShouldFail_WhenUnread()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();

			notification.Open();
		});
	}

	[Test]
	public void Open_ShouldFail_WhenOpened()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Open();
		});
	}

	[Test]
	public void Open_ShouldFail_WhenHidden()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Open();
		});
	}
}
