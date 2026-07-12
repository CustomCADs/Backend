using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly DesignerGetAllProductsHandler handler;
	private readonly DesignerGetAllProductsQuery request = new(
		Pagination: Query.Pagination,
		CallerId: ValidDesignerId,
		CategoryId: Query.CategoryId,
		Status: Status,
		Name: Query.Name,
		Sorting: Query.Sorting
	);

	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly ProductStatus Status = ProductStatus.Unchecked;
	private static readonly Product[] Products = [CreateProduct()];
	private static readonly ProductQuery Query = new(
		Pagination: new(1, Products.Length)
	);
	private static readonly Result<Product> Result = new(
		Count: Products.Length,
		Items: Products
	);


	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(It.IsAny<ProductQuery>(), false, ct))
			.ReturnsAsync(Result);

		reads.Setup(x => x.TagsByIdsAsync(It.Is<ProductId[]>(x => x.SequenceEqual(Products.Select(x => x.Id))), ct))
			.ReturnsAsync(Products.ToDictionary(x => x.Id, x => new[] { "Tag123" }));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.SequenceEqual(Products.Select(x => x.CreatorId))),
			ct
		)).ReturnsAsync(Products.ToDictionary(x => x.CreatorId, x => "Username123"));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetCategorByIdQuery>(x => x.Ids.SequenceEqual(Products.Select(x => x.CategoryId))),
			ct
		)).ReturnsAsync(Products.ToDictionary(x => x.CategoryId, x => "Cateogry123"));
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(
				It.IsAny<ProductQuery>(),
				false,
				ct
			),
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
				It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.SequenceEqual(Products.Select(x => x.CreatorId))),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<BatchGetCategorByIdQuery>(x => x.Ids.SequenceEqual(Products.Select(x => x.CategoryId))),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Products.Length).IsEqualTo(result.Count);
	}
}
