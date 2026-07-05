using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.ViewedProducts;
using CustomCADs.Modules.Accounts.Domain.Accounts.Entities;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProducts.ByName;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountViewedProductsByUsernameHandler handler;
	private readonly GetAccountViewedProductsByUsernameQuery request = new(ValidUsername);

	private readonly Mock<IAccountReads> reads = new();

	private static readonly ViewedProduct[] Expected = [];

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ViewedProductsByUsernameAsync(ValidUsername, ct))
			.ReturnsAsync(Expected);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.ViewedProductsByUsernameAsync(ValidUsername, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ViewedProductDto[] products = await handler.Handle(request, ct);

		// Assert
		await Assert.That(products.Select(x => x.Id)).IsEquivalentTo(Expected.Select(x => x.ProductId));
	}
}
