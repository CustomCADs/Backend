using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using CustomCADs.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

using static ActiveCartsData;

public class PurchaseActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly PurchaseActiveCartHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly string paymentMethodId = string.Empty;

	public PurchaseActiveCartHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

		reads.Setup(x => x.ExistsAsync(ValidBuyerId, ct))
			.ReturnsAsync(true);

		ActiveCartItem[] items = [
			CreateItem(productId: ProductId.New()),
			CreateItem(productId: ProductId.New()),
			CreateItem(productId: ProductId.New()),
		];
		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync(items);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetProductPricesByIdsQuery>(),
			ct
		)).ReturnsAsync(items.ToDictionary(x => x.ProductId, x => 0m));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.ExistsAsync(ValidBuyerId, ct), Times.Once());
		reads.Verify(x => x.AllAsync(ValidBuyerId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetProductPricesByIdsQuery>(),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreatePurchasedCartCommand>(x => x.BuyerId == ValidBuyerId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CartPurchased)
		), Times.Once());
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.IsAny<CartPaymentStartedApplicationEvent>()
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange
		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		payment.Verify(x => x.InitializeCartPayment(
			It.Is<string>(x => x == paymentMethodId),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.IsAny<PurchasedCartId>(),
			It.IsAny<decimal>(),
			It.IsAny<(string, int)>(),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializeCartPayment(
			It.Is<string>(x => x == paymentMethodId),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.IsAny<PurchasedCartId>(),
			It.IsAny<decimal>(),
			It.IsAny<(string, int)>(),
			ct
		)).ReturnsAsync(expected);

		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Act
		PaymentDto actual = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartForDelivery()
	{
		// Arrange
		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync([
				CreateItemWithDelivery(),
				CreateItem(),
				CreateItemWithDelivery(),
			]);

		PurchaseActiveCartCommand command = new(paymentMethodId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
