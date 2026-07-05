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
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Commands.Internal.Create;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly CreatePurchasedCartHandler handler;
	private readonly CreatePurchasedCartCommand request = new(ValidBuyerId, Items);

	private readonly Mock<IWrites<PurchasedCart>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly Dictionary<ActiveCartItemDto, decimal> Items = [];
	private static readonly ProductId[] ProductIds = [.. Items.Keys.Select(x => x.ProductId)];
	private static readonly Dictionary<ProductId, CadId> Cads = [];
	private static readonly CadId[] CadIds = [.. Cads.Select(x => x.Value)];

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<PurchasedCart>(x => x.BuyerId == ValidBuyerId),
			ct
		)).ReturnsAsync(CreateCart(id: ValidId));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetProductCadIdByIdQuery>(x => x.Ids == ProductIds),
			ct
		)).ReturnsAsync(Cads);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<BatchDuplicateCadByIdCommand>(x => x.Ids == CadIds),
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
				It.Is<BatchGetProductCadIdByIdQuery>(x => x.Ids == ProductIds),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<BatchDuplicateCadByIdCommand>(x => x.Ids == CadIds),
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
				It.Is<ProductsPurchasedApplicationEvent>(x => x.Ids == ProductIds)
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
