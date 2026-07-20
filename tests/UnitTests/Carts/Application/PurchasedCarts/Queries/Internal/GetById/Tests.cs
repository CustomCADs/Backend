using CustomCADs.Modules.Carts.Application.PurchasedCarts.Dtos;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly GetPurchasedCartByIdHandler handler;
	private readonly GetPurchasedCartByIdQuery request = new(ValidId, ValidBuyerId);

	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Buyer = "PDMatsaliev20";
	private readonly PurchasedCart cart = CreateCartWithItems(items: [
		CreateItem(productId: ProductId.New()),
		CreateItem(productId: ProductId.New()),
		CreateItem(productId: ProductId.New()),
	]);

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(Buyer);
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
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var cart = await handler.Handle(request, ct);

		// Assert
		// 	Cart properties
		using (Assert.Multiple())
		{
			await Assert.That(cart.Id).IsEqualTo(this.cart.Id);
			await Assert.That(cart.Total).IsEqualTo(this.cart.TotalCost);
			await Assert.That(cart.PurchasedAt).IsEqualTo(this.cart.PurchasedAt);
			await Assert.That(cart.PaymentStatus).IsEqualTo(this.cart.PaymentStatus);
			await Assert.That(cart.BuyerName).IsEqualTo(Buyer);
			await Assert.That(cart.ShipmentId).IsEqualTo(this.cart.ShipmentId);
		}

		// Cart items
		using (Assert.Multiple())
		{
			await Assert.That(cart.Items.Count).IsEqualTo(this.cart.Items.Count);
			foreach (PurchasedCartItemDto actual in cart.Items)
			{
				PurchasedCartItem expected = this.cart.Items.First(x => x.CartId == actual.CartId && x.ProductId == actual.ProductId);

				await Assert.That(actual.Quantity).IsEqualTo(expected.Quantity);
				await Assert.That(actual.ForDelivery).IsEqualTo(expected.ForDelivery);
				await Assert.That(actual.Price).IsEqualTo(expected.Price);
				await Assert.That(actual.Cost).IsEqualTo(expected.Cost);
				await Assert.That(actual.AddedAt).IsEqualTo(expected.AddedAt);
				await Assert.That(actual.ProductId).IsEqualTo(expected.ProductId);
				await Assert.That(actual.CartId).IsEqualTo(expected.CartId);
				await Assert.That(actual.CadId).IsEqualTo(expected.CadId);
				await Assert.That(actual.CustomizationId).IsEqualTo(expected.CustomizationId);
			}
		}
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as PurchasedCart);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCart(buyerId: AccountId.New()));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<PurchasedCart>>(() => handler.Handle(request, ct));
	}
}
