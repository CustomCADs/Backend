using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetByName;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.Internal.GetByName;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	private readonly GetTagByNameHandler handler;
	private readonly GetTagByNameQuery request = new(MinValidName);

	private readonly Mock<ITagReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByNameAsync(MinValidName, false, ct))
			.ReturnsAsync(CreateTag());
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByNameAsync(MinValidName, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Id).IsEqualTo(ValidId);
	}
}
