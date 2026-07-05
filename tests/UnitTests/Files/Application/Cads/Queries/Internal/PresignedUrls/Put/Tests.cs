using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetCadPresignedUrlPutHandler handler;
	private readonly GetCadPresignedUrlPutQuery request = new(ValidId, UploadRequest, Type, ValidOwnerId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private const string PresignedUrl = "presigned-url";
	private const FileContextType Type = FileContextType.Product;
	private static readonly UploadFileRequest UploadRequest = new(ValidContentType, "Batman.glb");

	public Tests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new PolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(CreateCad());

		storage.Setup(x => x.GetPresignedPutUrlAsync(ValidKey, UploadRequest)).ReturnsAsync(PresignedUrl);
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()),
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
			x => x.GetPresignedPutUrlAsync(ValidKey, UploadRequest),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		string url = await handler.Handle(request, ct);

		// Assert
		await Assert.That(url).IsEqualTo(PresignedUrl);
	}
}