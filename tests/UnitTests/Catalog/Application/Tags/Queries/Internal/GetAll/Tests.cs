using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetAll;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.Internal.GetAll;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	private readonly GetAllTagsHandler handler;
	private readonly GetAllTagsQuery request = new();

	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private static readonly Tag[] Tags = [
		CreateTag(MinValidName),
		CreateTag(MaxValidName)
	];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			It.IsAny<Func<Task<ICollection<Tag>>>>()
		)).ReturnsAsync(Tags);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Tag>>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		TagDto[] tags = await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(tags.Select(r => r.Id), Tags.Select(r => r.Id)),
			() => Assert.Equal(tags.Select(r => r.Name), Tags.Select(r => r.Name))
		);
	}
}
