using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.UserRole;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUserRole;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetUserRoleByIdHandler handler;
	private readonly GetUserRoleByIdQuery request = new(ValidId);

	private readonly Mock<IAccountReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		string actualRole = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Data.Roles.TestData.ValidName, actualRole);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
