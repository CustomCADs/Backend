using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly GetAllPurchasedCartsHandler handler;
	private readonly GetAllPurchasedCartsQuery request = new(Query.Pagination);

	private readonly Mock<IPurchasedCartReads> reads = new();

	private static readonly PurchasedCart[] Carts = [
		CreateCart(id: ValidId),
		CreateCart(id: ValidId),
	];
	private static readonly PurchasedCartQuery Query = new(
		Pagination: new(1, Carts.Length)
	);


	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(Query, false, ct))
			.ReturnsAsync(new Result<PurchasedCart>(
				Carts.Length,
				Carts
			));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(Query, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		int expectedCount = Carts.Length, actualCount = result.Count;
		PurchasedCartId[] expectedIds = [.. Carts.Select(x => x.Id)],
			actualIds = [.. result.Items.Select(x => x.Id)];

		Assert.Multiple(
			() => Assert.Equal(expectedCount, actualCount),
			() => Assert.Equal(expectedIds, actualIds)
		);
	}
}
