using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetAll;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly GetAllNotificationsHandler handler;
	private readonly GetAllNotificationsQuery request = new(
		Pagination: Query.Pagination,
		CallerId: ValidReceiverId,
		Sorting: Query.Sorting
	);

	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly Notification[] Notifications = [
		CreateNotification(id: ValidId, authorId: AccountId.New()),
		CreateNotification(id: ValidId, authorId: AccountId.New())
	];
	private static readonly NotificationQuery Query = new(
		Pagination: new(1, Notifications.Length)
	);
	private static readonly Result<Notification> Result = new(
		Count: Notifications.Length,
		Items: Notifications
	);

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(
			It.IsAny<NotificationQuery>(),
			false,
			ct
		)).ReturnsAsync(Result);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.Length == Notifications.Length),
			ct
		)).ReturnsAsync(Notifications.ToDictionary(x => x.AuthorId, x => "Username123"));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(
				It.IsAny<NotificationQuery>(),
				false,
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.Length == Notifications.Length),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Result<GetAllNotificationsDto> res = await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(res.Items.Select(r => r.Id), Result.Items.Select(r => r.Id)),
			() => Assert.Equal(res.Count, Result.Count)
		);
	}
}
