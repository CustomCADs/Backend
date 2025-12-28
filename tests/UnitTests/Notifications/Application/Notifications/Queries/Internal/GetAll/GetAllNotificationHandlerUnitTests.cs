using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetAll;

using static NotificationsData;

public class GetAllNotificationHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly GetAllNotificationsHandler handler;
	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Notification[] notifications = [
		CreateNotification(ValidId, authorId: AccountId.New()),
		CreateNotification(ValidId, authorId: AccountId.New())
	];
	private readonly NotificationQuery query;
	private readonly Result<Notification> result;

	public GetAllNotificationHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		query = new(
			Pagination: new(1, notifications.Length)
		);
		result = new(
			Count: notifications.Length,
			Items: notifications
		);

		reads.Setup(x => x.AllAsync(
			It.IsAny<NotificationQuery>(),
			false,
			ct
		)).ReturnsAsync(result);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Length == notifications.Length),
			ct
		)).ReturnsAsync(notifications.ToDictionary(x => x.AuthorId, x => "Username123"));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllNotificationsQuery query = new(
			Pagination: this.query.Pagination,
			CallerId: ValidReceiverId,
			Sorting: this.query.Sorting
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(
			It.IsAny<NotificationQuery>(),
			false,
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetAllNotificationsQuery query = new(
			Pagination: this.query.Pagination,
			CallerId: ValidReceiverId,
			Sorting: this.query.Sorting
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Length == notifications.Length),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllNotificationsQuery query = new(
			Pagination: this.query.Pagination,
			CallerId: ValidReceiverId,
			Sorting: this.query.Sorting
		);

		// Act
		Result<GetAllNotificationsDto> result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(result.Items.Select(r => r.Id), this.result.Items.Select(r => r.Id)),
			() => Assert.Equal(result.Count, this.result.Count)
		);
	}
}
