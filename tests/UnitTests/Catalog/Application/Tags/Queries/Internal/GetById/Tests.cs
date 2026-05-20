using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetById;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.Internal.GetById;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	private readonly GetTagByIdHandler handler;
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private readonly static TagId id = ValidId;

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			id,
			It.IsAny<Func<Task<Tag>>>()
		)).ReturnsAsync(CreateTag());
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetTagByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(id, It.IsAny<Func<Task<Tag>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetTagByIdQuery query = new(id);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidId, result.Id);
	}
}
