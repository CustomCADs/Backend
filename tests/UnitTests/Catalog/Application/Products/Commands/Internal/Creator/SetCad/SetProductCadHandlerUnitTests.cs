using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCad;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.SetCad;

using static ProductsData;

public class SetProductCadHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly SetProductCadHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string ContentType = "content-type";
	private const decimal Volume = 0;

	public SetProductCadHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateProduct());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetProductCadCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			Volume: Volume,
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
		SetProductCadCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			Volume: Volume,
			CallerId: ValidCreatorId
		);
		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<EditCadCommand>(x => x.Id == ValidCadId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		SetProductCadCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			Volume: Volume,
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

		SetProductCadCommand command = new(
			Id: ValidId,
			ContentType: ContentType,
			Volume: Volume,
			CallerId: ValidCreatorId
		);
		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
