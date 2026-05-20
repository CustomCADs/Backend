using CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Edit;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	private readonly EditTagHandler handler;
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private static readonly Tag tag = CreateTag();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(tag);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, tag),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}


	[Fact]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Tag);
		EditTagCommand command = new(ValidId, MaxValidName);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
