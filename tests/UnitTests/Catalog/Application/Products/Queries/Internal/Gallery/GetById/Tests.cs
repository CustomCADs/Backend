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

	[Test]
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

	[Test]
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

	[Test]
	[Arguments(false, false)]
	[Arguments(true, false)]
	[Arguments(false, true)]
	[Arguments(true, true)]
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

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(result.Id).IsEqualTo(product.Id);
			await Assert.That(result.Name).IsEqualTo(product.Name);
			await Assert.That(result.Description).IsEqualTo(product.Description);
			await Assert.That(result.Price).IsEqualTo(product.Price);
			await Assert.That(result.Category.Id).IsEqualTo(product.CategoryId);
		}
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenStatusIsNotValid()
	{
		// Arrange
		product.Report(ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Product>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(() => handler.Handle(request, ct));
	}
}
