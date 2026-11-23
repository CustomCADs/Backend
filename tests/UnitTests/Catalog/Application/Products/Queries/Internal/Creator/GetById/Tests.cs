using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetById;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly CreatorGetProductByIdHandler handler;
	private readonly CreatorGetProductByIdQuery request = new(ValidId, ValidCreatorId);

	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);
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
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateProduct(creatorId: AccountId.New()));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(() => handler.Handle(request, ct));
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
