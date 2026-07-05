using CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Exists;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Exists;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly GetCustomizationExistsByIdHandler handler;
	private readonly GetCustomizationExistsByIdQuery request = new(ValidId);

	private readonly Mock<ICustomizationReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct))
			.ReturnsAsync(true);
	}

	[Test]
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

	[Test]
	[Arguments(true)]
	[Arguments(false)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct))
			.ReturnsAsync(exists);

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEqualTo(exists);
	}
}