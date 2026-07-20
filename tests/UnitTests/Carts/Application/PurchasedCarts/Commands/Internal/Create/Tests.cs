using CustomCADs.Modules.Carts.Application.ActiveCarts.Dtos;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Commands.Internal.Create;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly CreatePurchasedCartHandler handler;
	private readonly CreatePurchasedCartCommand request = new(ValidBuyerId, Items.ToDictionary(x => x.Value, x => Prices[x.Key]));

	private readonly Mock<IWrites<PurchasedCart>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly PurchasedCart cart = CreateCart();
	private static readonly Dictionary<string, ActiveCartItemDto> Items = new()
	{
		["item-1"] = new(1, false, "buyer-1", DateTimeOffset.UtcNow, AccountId.New(), ProductId.New(), null),
		["item-2"] = new(2, true, "buyer-2", DateTimeOffset.UtcNow, AccountId.New(), ProductId.New(), CustomizationId.New()),
		["item-3"] = new(3, false, "buyer-3", DateTimeOffset.UtcNow, AccountId.New(), ProductId.New(), null),
	};
	private static readonly Dictionary<string, decimal> Prices = new()
	{
		["item-1"] = 1.1m,
		["item-2"] = 2.2m,
		["item-3"] = 3.3m,
	};
	private static readonly ProductId[] ProductIds = [.. Items.Values.Select(x => x.ProductId)];
	private static readonly Dictionary<ProductId, CadId> Cads = new()
	{
		[Items["item-1"].ProductId] = CadId.New(),
		[Items["item-2"].ProductId] = CadId.New(),
		[Items["item-3"].ProductId] = CadId.New(),
	};
	private static readonly CadId[] CadIds = [.. Cads.Select(x => x.Value)];

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<PurchasedCart>(x => x.BuyerId == ValidBuyerId),
			ct
		)).ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetProductCadIdByIdQuery>(x => x.Ids.SequenceEqual(ProductIds)),
			ct
		)).ReturnsAsync(Cads);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<BatchDuplicateCadByIdCommand>(x => x.Ids.SequenceEqual(CadIds)),
			ct
		)).ReturnsAsync(Cads.ToDictionary(x => x.Value, _ => CadId.New()));
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
				It.Is<PurchasedCart>(x => x.BuyerId == ValidBuyerId),
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
				It.Is<BatchGetProductCadIdByIdQuery>(x => x.Ids.SequenceEqual(ProductIds)),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<BatchDuplicateCadByIdCommand>(x => x.Ids.SequenceEqual(CadIds)),
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
				It.Is<ProductsPurchasedApplicationEvent>(x => x.Ids.SequenceEqual(ProductIds))
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		PurchasedCartId result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenPurchasedCartNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetAccountExistsByIdQuery>(),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(() => handler.Handle(request, ct));
	}
}
