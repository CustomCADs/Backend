using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Modules.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentStarted;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly PurchaseCustomWithDeliveryHandler handler;
	private readonly PurchaseCustomWithDeliveryCommand request = new(
		Id: ValidId,
		Count: 1,
		CustomizationId: ValidCustomizationId,
		PaymentMethodId: string.Empty,
		ShipmentService: string.Empty,
		CallerId: ValidBuyerId,
		Address: Address,
		Contact: Contact
	);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly AddressDto Address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly ContactDto Contact = new(null, null);
	private readonly Custom custom = CreateCustom(forDelivery: true);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationCostByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(0m);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(true);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
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
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId || x.Id == ValidDesignerId),
				ct
			),
			Times.Exactly(2)
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCustomizationCostByIdQuery>(x => x.Id == ValidCustomizationId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		payment.Verify(
			x => x.InitializeCustomPayment(
				It.Is<string>(x => string.IsNullOrEmpty(x)),
				It.Is<AccountId>(x => x == ValidBuyerId),
				It.Is<CustomId>(x => x == ValidId),
				It.Is<decimal>(x => x == ValidPrice),
				It.Is<(string, string Name, string)>(x => x.Name == custom.Name),
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
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomCompleted)
			),
			Times.Once()
		);
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<CustomDeliveryRequestedApplicationEvent>(x => x.CustomId == custom.Id)
			),
			Times.Once()
		);
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.IsAny<CustomPaymentStartedApplicationEvent>()
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializeCustomPayment(
			It.Is<string>(x => string.IsNullOrEmpty(x)),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.Is<CustomId>(x => x == ValidId),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<(string, string Name, string)>(x => x.Name == custom.Name),
			ct
		)).ReturnsAsync(expected);

		// Act
		PaymentDto actual = await handler.Handle(request, ct);

		// Assert
		await Assert.That(actual).IsEqualTo(expected);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(() => handler.Handle(request with { CallerId = new() }, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNotAccepted()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomException>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNotFinished()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomException>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNoForDelivery()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCustom(buyerId: ValidBuyerId, forDelivery: false));

		// Assert
		await Assert.ThrowsAsync<CustomException>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.Handle(request, ct));
	}


	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId), ct))
			.ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.Handle(request, ct));
	}
}
