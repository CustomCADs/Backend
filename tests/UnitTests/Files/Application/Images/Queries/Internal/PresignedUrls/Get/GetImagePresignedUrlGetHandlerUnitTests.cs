using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Get;

using static ImagesData;

public class GetImagePresignedUrlGetHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlGetHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private static readonly Image image = CreateImage();
	private const FileContextType Type = FileContextType.Product;
	private const string PresignedUrl = "PresignedUrl";

	public GetImagePresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new ImageDownloadPolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(image);

		storage.Setup(x => x.GetPresignedGetUrlAsync(image.Key))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetImagePresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetImagePresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(image.Key), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(image.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}
}
