using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Exists;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists.ByUsername;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountExistsByUsernameHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountExistsByUsernameQuery query = new(ValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.ExistsByUsernameAsync(ValidUsername, ct),
			Times.Once()
		);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByUsernameAsync(ValidUsername, ct)).ReturnsAsync(exists);
		GetAccountExistsByUsernameQuery query = new(ValidUsername);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
