using CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;

using static CadsData;

public class GetCadPresignedUrlPutHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPutHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private const string PresignedUrl = "presigned-url";
	private const FileContextType Type = FileContextType.Product;
	private static readonly Cad cad = CreateCad();
	private static readonly UploadFileRequest req = new(ValidContentType, "Batman.glb");

	public GetCadPresignedUrlPutHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new CadReplacePolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(CreateCad());

		storage.Setup(x => x.GetPresignedPutUrlAsync(cad.Key, req)).ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCadPresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetCadPresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPutUrlAsync(cad.Key, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlPutQuery query = new(ValidId, req, Type, ValidOwnerId);

		// Act
		string url = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(PresignedUrl, url);
	}
}
