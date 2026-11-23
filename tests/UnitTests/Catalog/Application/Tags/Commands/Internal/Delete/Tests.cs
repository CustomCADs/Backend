using CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Delete;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Delete;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	private readonly DeleteTagHandler handler;
	private readonly DeleteTagCommand request = new(ValidId);

	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<ITagWrites> writes = new();
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private readonly Tag tag = CreateTag();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(tag);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.Remove(tag),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Tag);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(() => handler.Handle(request, ct));
	}
}
