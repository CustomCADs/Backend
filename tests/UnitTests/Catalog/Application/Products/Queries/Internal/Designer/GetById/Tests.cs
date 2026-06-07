using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetById;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Designer.GetById;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly DesignerGetProductByIdHandler handler;
	private readonly DesignerGetProductByIdQuery request = new(ValidId, ValidDesignerId);

	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
				ct
			),
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
			() => Assert.Equal(product.Id, result.Id),
			() => Assert.Equal(product.Name, result.Name),
			() => Assert.Equal(product.Description, result.Description),
			() => Assert.Equal(product.Price, result.Price),
			() => Assert.Equal(product.CategoryId, result.Category.Id)
		);
	}

	[Fact]
	public async Task Handle_ShouldNotThrowException_WhenUnauthorizedAccess()
	{
		// Arrange

		// Act
		Exception? ex = await Record.ExceptionAsync(
			() => handler.Handle(request with { CallerId = ValidCreatorId }, ct)
		);

		// Assert
		Assert.Null(ex);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccessButValidated()
	{
		// Arrange
		product.Validate(ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			() => handler.Handle(request with { CallerId = ValidCreatorId }, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
