using CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.BatchCadIdById;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly BatchGetProductCadIdByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();

	private readonly ProductId[] ids = [ValidId, ValidId, ValidId];
	private readonly ProductQuery query;
	private readonly Result<Product> result;
	private readonly Product[] products = [
		CreateProduct(MinValidName, MinValidDescription, MinValidPrice, id: ProductId.New()),
		CreateProduct(MaxValidName, MaxValidDescription, MaxValidPrice, id: ProductId.New()),
	];

	public Tests()
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
		BatchGetProductCadIdByIdQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(this.query, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		BatchGetProductCadIdByIdQuery query = new(ids);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.True(result.ElementAt(0).Value == ValidCadId),
			() => Assert.True(result.ElementAt(1).Value == ValidCadId)
		);
	}
}
