using CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.BatchCadIdById;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly BatchGetProductCadIdByIdHandler handler;
	private readonly BatchGetProductCadIdByIdQuery request = new(Ids);

	private readonly Mock<IProductReads> reads = new();

	private static readonly Product[] Products = [
		CreateProduct(MinValidName, MinValidDescription, MinValidPrice, id: ProductId.New()),
		CreateProduct(MaxValidName, MaxValidDescription, MaxValidPrice, id: ProductId.New()),
	];
	private static readonly ProductId[] Ids = [.. Products.Select(x => x.Id)];
	private static readonly ProductQuery Query = new(
		Ids: Ids,
		Pagination: new(Limit: Ids.Length)
	);
	private static readonly Result<Product> Result = new(
		Count: Products.Length,
		Items: Products
	);

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.AllAsync(Query, false, ct))
			.ReturnsAsync(Result);
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
		Assert.Multiple(
			() => Assert.True(result[Ids[0]] == Products[0].CadId),
			() => Assert.True(result[Ids[1]] == Products[1].CadId)
		);
	}
}
