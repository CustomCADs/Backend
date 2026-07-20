using CustomCADs.Modules.Files.Application.Images.Events.Application;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Images.Events.Application;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly ProductDeletedHandler handler;
	private readonly ProductDeletedApplicationEvent request = new(default, ValidId, default);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IWrites<Image>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private readonly Image image = CreateImage();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, storage.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(image);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

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
		await handler.HandleAsync(request);

		// Assert
		writes.Verify(
			x => x.Remove(image),
			Times.Once()
		);
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
		await handler.HandleAsync(request);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		storage.Verify(
			x => x.DeleteFileAsync(image.Key, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Image);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(() => handler.HandleAsync(request));
	}
}
