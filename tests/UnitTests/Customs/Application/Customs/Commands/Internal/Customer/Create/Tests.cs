using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CreateCustomHandler handler;
	private readonly CreateCustomCommand request = new(
		Name: MaxValidName,
		Description: MaxValidDescription,
		ForDelivery: true,
		CallerId: ValidBuyerId,
		CategoryId: ValidCategoryId
	);


	private readonly Mock<IWrites<Custom>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Custom>(x =>
				x.Name == MaxValidName &&
				x.Description == MaxValidDescription &&
				x.ForDelivery &&
				x.BuyerId == ValidBuyerId
			),
			ct
		)).ReturnsAsync(CreateCustom(id: ValidId));

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetAccountExistsByIdQuery>(),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountIdsByRoleQuery>(x => x.Role == "Designer"),
			ct
		)).ReturnsAsync([]);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Custom>(x =>
					x.Name == MaxValidName &&
					x.Description == MaxValidDescription &&
					x.ForDelivery &&
					x.BuyerId == ValidBuyerId
				),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountIdsByRoleQuery>(x => x.Role == "Designer"),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomCreated)
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CustomId id = await handler.Handle(request, ct);

		// Assert
		await Assert.That(id).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenBuyerNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.Handle(request, ct));
	}
}