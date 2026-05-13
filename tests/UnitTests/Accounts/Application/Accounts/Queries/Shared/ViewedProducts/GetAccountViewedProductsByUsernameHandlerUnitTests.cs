using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.ViewedProducts;
using CustomCADs.Modules.Accounts.Domain.Accounts.Entities;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProducts;

using static AccountsData;

public class GetAccountViewedProductsByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountViewedProductsByUsernameHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly ViewedProduct[] expected = [];

	public GetAccountViewedProductsByUsernameHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ViewedProductsByUsernameAsync(ValidUsername, ct))
			.ReturnsAsync(expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountViewedProductsByUsernameQuery query = new(ValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ViewedProductsByUsernameAsync(ValidUsername, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAccountViewedProductsByUsernameQuery query = new(ValidUsername);

		// Act
		ViewedProductDto[] products = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(expected.Select(x => x.ProductId), products.Select(x => x.Id));
	}
}
