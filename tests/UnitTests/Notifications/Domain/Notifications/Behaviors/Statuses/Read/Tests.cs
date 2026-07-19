using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Read;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	public async Task Read_ShouldSucceed_WhenUnread()
	{
		Notification notification = CreateNotification();
		notification.Read();
		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Read);
	}

	[Test]
	public void Read_ShouldFail_WhenRead()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();

			notification.Read();
		});
	}

	[Test]
	public void Read_ShouldFail_WhenOpened()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Read();
		});
	}

	[Test]
	public void Read_ShouldFail_WhenHidden()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Read();
		});
	}
}
