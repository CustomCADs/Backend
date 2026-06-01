using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductViewed;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GalleryGetProductByIdHandler handler;
	private readonly GalleryGetProductByIdQuery request = new(ValidId, ValidCreatorId, Viewed: false);

	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Product product = CreateProduct(id: ValidId);

	public Tests()
	{
		handler = new(reads.Object, sender.Object, raiser.Object);

		product.Validate(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		CoordinatesDto coords = new(0, 0, 0);
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
				It.Is<GetUsernameByIdQuery>(x => x.Id == product.CreatorId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryNameByIdQuery>(x => x.Id == product.CategoryId),
				ct
			),
			Times.Once()
		);
	}

	[Theory]
	[InlineData(false, false)]
	[InlineData(true, false)]
	[InlineData(false, true)]
	[InlineData(true, true)]
	public async Task Handle_ShouldRaiseEvents(bool authenticatedUser, bool viewed)
	{
		// Arrange
		AccountId creatorId = authenticatedUser ? ValidCreatorId : new();

		// Act
		await handler.Handle(request with { CallerId = creatorId, Viewed = viewed }, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<ProductViewedApplicationEvent>(x => x.Id == product.Id)
			),
			Times.Exactly((authenticatedUser && viewed) ? 1 : 0)
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
	public async Task Handle_ShouldThrowException_WhenStatusIsNotValid()
	{
		// Arrange
		product.Report(ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Product>>(
			// Act
			() => handler.Handle(request, ct)
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
