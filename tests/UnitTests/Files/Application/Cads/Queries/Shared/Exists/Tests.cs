using CustomCADs.Modules.Files.Application.Cads.Queries.Shared;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.Exists;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly CadExistsByIdHandler handler;
	private readonly CadExistsByIdQuery request = new(ValidId);

	private readonly Mock<ICadReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.ExistsByIdAsync(ValidId, ct),
			Times.Once()
		);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
