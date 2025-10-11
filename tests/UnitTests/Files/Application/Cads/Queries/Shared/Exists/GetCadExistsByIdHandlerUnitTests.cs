using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.Exists;

using static CadsData;

public class CadExistsByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly CadExistsByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();

	public CadExistsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CadExistsByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ExistsByIdAsync(ValidId, ct), Times.Once());
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);
		CadExistsByIdQuery query = new(ValidId);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
