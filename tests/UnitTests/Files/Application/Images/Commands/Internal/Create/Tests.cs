using CustomCADs.Modules.Files.Application.Images.Commands.Internal.Create;
using CustomCADs.Modules.Files.Domain.Repositories;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Internal.Create;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly CreateImageHandler handler;
	private readonly CreateImageCommand request = new(ValidKey, ValidContentType, ValidOwnerId);

	private readonly Mock<IWrites<Image>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Image>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		)).ReturnsAsync(CreateImage(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Image>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(
				ValidId,
				It.Is<Image>(x => x.Key == ValidKey && x.ContentType == ValidContentType)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ImageId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
