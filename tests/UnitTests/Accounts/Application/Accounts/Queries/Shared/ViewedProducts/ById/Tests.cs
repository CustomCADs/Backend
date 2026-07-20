using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.ViewedProducts;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProducts.ById;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountViewedProductHandler handler;
	private readonly GetAccountViewedProductQuery request = new(ValidId, ValidProductId);

	private readonly Mock<IAccountReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ViewedProductsByIdAsync(ValidId, ct))
			.ReturnsAsync([]);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.ViewedProductsByIdAsync(ValidId, ct),
			Times.Once()
		);
	}

	[Test]
	[Arguments(true)]
	[Arguments(false)]
	public async Task Handle_ShouldReturnResult(bool expected)
	{
		// Arrange
		if (expected)
		{
			reads.Setup(x => x.ViewedProductsByIdAsync(ValidId, ct))
				.ReturnsAsync([ValidProductId]);
		}

		// Act
		bool actual = await handler.Handle(request, ct);

		// Assert
		await Assert.That(actual).IsEqualTo(expected);
	}
}