using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentStarted;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.Normal;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly PurchaseCustomHandler handler;
	private readonly PurchaseCustomCommand request = new(ValidId, PaymentMethodId, ValidBuyerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const string PaymentMethodId = "payment-method-id";
	private readonly Custom custom = CreateCustom(
		buyerId: ValidBuyerId
	);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
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

	[Fact]
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
	}

	[Fact]
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
				It.IsAny<CustomPaymentStartedApplicationEvent>()
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		payment.Verify(
			x => x.InitializeCustomPayment(
				It.Is<string>(x => x == PaymentMethodId),
				It.Is<AccountId>(x => x == custom.BuyerId),
				It.Is<CustomId>(x => x == custom.Id),
				It.Is<decimal>(x => x == ValidPrice),
				It.Is<(string, string Name, string)>(x => x.Name == custom.Name),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializeCustomPayment(
			It.Is<string>(x => x == PaymentMethodId),
			It.Is<AccountId>(x => x == custom.BuyerId),
			It.Is<CustomId>(x => x == custom.Id),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<(string, string Name, string)>(x => x.Name == custom.Name),
			ct
		)).ReturnsAsync(expected);

		// Act
		PaymentDto actual = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			() => handler.Handle(request with { CallerId = new() }, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotAccepted()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFinished()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId);
		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenForDelivery()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
