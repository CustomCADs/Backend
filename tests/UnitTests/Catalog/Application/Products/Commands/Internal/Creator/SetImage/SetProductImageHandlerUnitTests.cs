using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetImage;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.SetImage;

using static ProductsData;

public class SetProductImageHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly SetProductImageHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string ContentType = "content-type";

	public SetProductImageHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateProduct());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetProductImageCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		SetProductImageCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			CallerId: ValidCreatorId
		);
		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<EditImageCommand>(x => x.Id == ValidImageId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		SetProductImageCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			CallerId: ValidDesignerId
		);
		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		SetProductImageCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			CallerId: ValidCreatorId
		);
		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
