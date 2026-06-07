using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Put;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly GetImagePresignedUrlPutHandler handler;
	private readonly GetImagePresignedUrlPutQuery request = new(ValidId, UploadRequest, Type, ValidOwnerId);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private const string PresignedUrl = "presigned-url";
	private const FileContextType Type = FileContextType.Product;
	private static readonly UploadFileRequest UploadRequest = new(ValidContentType, "Batman.glb");

	public Tests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new PolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(CreateImage());

		storage.Setup(x => x.GetPresignedPutUrlAsync(ValidKey, UploadRequest)).ReturnsAsync(PresignedUrl);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		storage.Verify(
			x => x.GetPresignedPutUrlAsync(ValidKey, UploadRequest),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		string url = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(PresignedUrl, url);
	}
}
