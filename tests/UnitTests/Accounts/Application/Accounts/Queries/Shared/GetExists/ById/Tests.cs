using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Exists;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists.ById;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountExistsByIdHandler handler;
	private readonly GetAccountExistsByIdQuery request = new(ValidId);

	private readonly Mock<IAccountReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
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
	[InlineData(true)]
	[InlineData(false)]
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
