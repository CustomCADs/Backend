using CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Put;

using static ImagesData;

public class GetImagePresignedUrlPutHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlPutHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private const string PresignedUrl = "presigned-url";
	private const FileContextType Type = FileContextType.Product;
	private static readonly Image image = CreateImage();
	private static readonly UploadFileRequest req = new(ValidContentType, "Batman.glb");

	public GetImagePresignedUrlPutHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new ImageReplacePolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(CreateImage());

		storage.Setup(x => x.GetPresignedPutUrlAsync(image.Key, req)).ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetImagePresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

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
		GetImagePresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPutUrlAsync(image.Key, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

		// Act
		string url = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(PresignedUrl, url);
	}
}
