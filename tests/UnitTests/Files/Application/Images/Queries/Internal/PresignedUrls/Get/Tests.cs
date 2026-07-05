using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Get;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly GetImagePresignedUrlGetHandler handler;
	private readonly GetImagePresignedUrlGetQuery request = new(ValidId, Type, ValidOwnerId);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private readonly Image image = CreateImage();
	private const FileContextType Type = FileContextType.Product;
	private const string PresignedUrl = "PresignedUrl";

	public Tests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new PolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(image);

		storage.Setup(x => x.GetPresignedGetUrlAsync(image.Key))
			.ReturnsAsync(PresignedUrl);
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		storage.Verify(
			x => x.GetPresignedGetUrlAsync(image.Key),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var (Url, ContentType) = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(ContentType).IsEqualTo(image.ContentType);
			await Assert.That(Url).IsEqualTo(PresignedUrl);
		}
	}
}
