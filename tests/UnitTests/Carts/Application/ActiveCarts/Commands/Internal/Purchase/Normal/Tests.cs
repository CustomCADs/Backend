using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
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

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly PurchaseActiveCartHandler handler;
	private readonly PurchaseActiveCartCommand request = new(PaymentMethodId, ValidBuyerId);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly string PaymentMethodId = string.Empty;

	public Tests()
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
			It.IsAny<BatchGetProductPriceByIdQuery>(),
			ct
		)).ReturnsAsync(items.ToDictionary(x => x.ProductId, x => 0m));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.ExistsAsync(ValidBuyerId, ct),
			Times.Once()
		);
		reads.Verify(
			x => x.AllAsync(ValidBuyerId, false, ct),
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
				It.IsAny<BatchGetProductPriceByIdQuery>(),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<CreatePurchasedCartCommand>(x => x.BuyerId == ValidBuyerId),
				ct
			),
			Times.Once()
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
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CartPurchased)
			),
			Times.Once()
		);
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.IsAny<CartPaymentStartedApplicationEvent>()
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
			x => x.InitializeCartPayment(
				It.Is<string>(x => x == PaymentMethodId),
				It.Is<AccountId>(x => x == ValidBuyerId),
				It.IsAny<PurchasedCartId>(),
				It.IsAny<decimal>(),
				It.IsAny<(string, int)>(),
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
		payment.Setup(x => x.InitializeCartPayment(
			It.Is<string>(x => x == PaymentMethodId),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.IsAny<PurchasedCartId>(),
			It.IsAny<decimal>(),
			It.IsAny<(string, int)>(),
			ct
		)).ReturnsAsync(expected);

		// Act
		PaymentDto actual = await handler.Handle(request, ct);

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

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
