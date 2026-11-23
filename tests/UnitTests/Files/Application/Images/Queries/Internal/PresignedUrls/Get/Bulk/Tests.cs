using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Get.Bulk;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Get.Bulk;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly GetImagesPresignedUrlGetHandler handler;
	private readonly GetImagesPresignedUrlGetQuery request = new(Ids, Type, ValidOwnerId);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private readonly Image image = CreateImage();
	private static readonly ImageId[] Ids = [ValidId];
	private const FileContextType Type = FileContextType.Product;
	private const string PresignedUrl = "PresignedUrl";

	public Tests()
	{
		handler = new(reads.Object, storage.Object, cache.Object, policies: [new PolicyMock()]);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()))
			.Returns(async (ImageId id, Func<Task<Image>> factory) => await factory());

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(image);

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
			Times.Exactly(Ids.Length)
		);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Exactly(Ids.Length)
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
			Times.Exactly(Ids.Length)
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var images = await handler.Handle(request, ct);

		// Assert
		foreach ((string Url, string ContentType) in images)
		{
			using (Assert.Multiple())
			{
				await Assert.That(ContentType).IsEqualTo(image.ContentType);
				await Assert.That(Url).IsEqualTo(PresignedUrl);
			}
		}
	}
}
