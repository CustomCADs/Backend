using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses;

public class NotificationReadUnitTests : NotificationsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Notification>> expectValidationException
		= Assert.Throws<CustomValidationException<Notification>>;

	[Fact]
	public void Read_ShouldSucceed_WhenUnread()
	{
		Notification notification = CreateNotification();
		notification.Read();
		Assert.Equal(NotificationStatus.Read, notification.Status);
	}

	[Fact]
	public void Read_ShouldFail_WhenRead()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();

			notification.Read();
		});
	}

	[Fact]
	public void Read_ShouldFail_WhenOpened()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Read();
		});
	}

	[Fact]
	public void Read_ShouldFail_WhenHidden()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Read();
		});
	}
}
