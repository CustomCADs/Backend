using CustomCADs.Modules.Files.Application.Images.Commands.Internal.Edit;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Internal.Edit;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly EditImageHandler handler;
	private readonly EditImageCommand request = new(ValidId, ValidContentType, ValidOwnerId);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private readonly Image image = CreateImage();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(image);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, image),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldModifyImage()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		await Assert.That(image.Key).IsEqualTo(ValidKey);
		await Assert.That(image.ContentType).IsEqualTo(ValidContentType);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Image);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(() => handler.Handle(request, ct));
	}
}
