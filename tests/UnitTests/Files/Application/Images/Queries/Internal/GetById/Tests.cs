using CustomCADs.Modules.Files.Application.Images.Dtos;
using CustomCADs.Modules.Files.Application.Images.Queries.Internal.GetById;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.GetById;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	private readonly GetImageByIdHandler handler;
	private readonly GetImageByIdQuery request = new(ValidId);

	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly Image image = CreateImage();

	public Tests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()))
			.Returns(async (ImageId id, Func<Task<Image>> factory) => await factory());

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(image);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == image.OwnerId)
		)).ReturnsAsync("John Kiriakou");
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(It.Is<GetUsernameByIdQuery>(x => x.Id == image.OwnerId)),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ImageDto result = await handler.Handle(request).ConfigureAwait(false);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(result.Id).IsEqualTo(image.Id);
			await Assert.That(result.Key).IsEqualTo(image.Key);
			await Assert.That(result.ContentType).IsEqualTo(image.ContentType);
			await Assert.That(result.OwnerId).IsEqualTo(image.OwnerId);
		}
	}
}
