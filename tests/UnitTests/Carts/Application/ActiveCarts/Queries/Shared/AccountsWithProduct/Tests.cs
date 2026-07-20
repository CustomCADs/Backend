using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Shared.AccountsWithProduct;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Shared.AccountsWithProduct;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly GetAccountsWithProductInCartHandler handler;
	private readonly GetAccountsWithProductInCartQuery request = new(ValidProductId);

	private readonly Mock<IActiveCartReads> reads = new();

	private static readonly AccountId[] AccountIds = [
		AccountId.New(),
		AccountId.New(),
		AccountId.New(),
	];

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.AccountsWithAsync(ValidProductId, ct)).ReturnsAsync(AccountIds);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AccountsWithAsync(ValidProductId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		AccountId[] ids = await handler.Handle(request, ct);

		// Assert
		await Assert.That(ids).IsEquivalentTo(AccountIds);
	}
}
