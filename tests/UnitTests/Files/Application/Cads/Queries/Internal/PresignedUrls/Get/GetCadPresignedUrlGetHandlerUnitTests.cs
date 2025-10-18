using CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;

using static CadsData;

public class GetCadPresignedUrlGetHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlGetHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly Cad cad = CreateCad();
	private const FileContextType Type = FileContextType.Product;
	private const string PresignedUrl = "PresignedUrl";

	public GetCadPresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new CadDownloadPolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCadPresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

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
		GetCadPresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlGetQuery query = new(ValidId, Type, ValidOwnerId);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(cad.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}
}
