using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetCadPresignedUrlGetHandler handler;
	private readonly GetCadPresignedUrlGetQuery request = new(ValidId, Type, ValidOwnerId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private readonly Cad cad = CreateCad();
	private const FileContextType Type = FileContextType.Product;
	private const string PresignedUrl = "PresignedUrl";

	public Tests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new PolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType))
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
			x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType),
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
		await Assert.That(ContentType).IsEqualTo(cad.ContentType);
		await Assert.That(Url).IsEqualTo(PresignedUrl);
	}
}
