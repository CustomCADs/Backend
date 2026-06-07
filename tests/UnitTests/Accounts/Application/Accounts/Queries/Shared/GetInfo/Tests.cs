using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Info;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetInfo;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountInfoByUsernameHandler handler;
	private readonly GetAccountInfoByUsernameQuery request = new(ValidUsername);

	private readonly Mock<IAccountReads> reads = new();

	private readonly Account account = CreateAccount();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByUsernameAsync(ValidUsername, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		AccountInfoDto info = await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(account.Id, info.Id),
			() => Assert.Equal(account.CreatedAt, info.CreatedAt),
			() => Assert.Equal(account.TrackViewedProducts, info.TrackViewedProducts),
			() => Assert.Equal(account.FirstName, info.FirstName),
			() => Assert.Equal(account.LastName, info.LastName)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct)).ReturnsAsync(null as Account);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
