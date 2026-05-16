using CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.BatchPricesById;

using static ProductsData;

public class BatchGetProductPriceByIdHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly BatchGetProductPriceByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();

	private readonly ProductId[] ids = [ValidId, ValidId, ValidId];
	private readonly ProductQuery query;
	private readonly Result<Product> result;
	private readonly Product[] products = [
		CreateProductWithId(MinValidName, MinValidDescription, MinValidPrice, id: ProductId.New()),
		CreateProductWithId(MaxValidName, MaxValidDescription, MaxValidPrice, id: ProductId.New()),
	];

	public BatchGetProductPriceByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		query = new(
			Ids: ids,
			Pagination: new(Limit: ids.Length)
		);
		result = new(
			Count: products.Length,
			Items: products
		);
		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(result);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		BatchGetProductPriceByIdQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		BatchGetProductPriceByIdQuery query = new(ids);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.True(result.ElementAt(0).Value == MinValidPrice),
			() => Assert.True(result.ElementAt(1).Value == MaxValidPrice)
		);
	}
}
