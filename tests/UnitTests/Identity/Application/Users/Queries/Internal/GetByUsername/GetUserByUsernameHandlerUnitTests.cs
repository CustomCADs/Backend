using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Queries.Internal.GetByUsername;

using static UsersData;

public class GetUserByUsernameHandlerUnitTests : UsersBaseUnitTests
{
	private readonly GetUserByUsernameHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly User user = CreateUserWithId();

	public GetUserByUsernameHandlerUnitTests()
	{
		handler = new(service.Object, sender.Object);

		service.Setup(x => x.GetByAccountIdAsync(user.AccountId))
			.ReturnsAsync(user);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(new AccountInfoDto(ValidAccountId, DateTimeOffset.UtcNow, true, null, null));
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		GetUserByUsernameQuery query = new(user.AccountId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		service.Verify(x => x.GetByAccountIdAsync(user.AccountId), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetUserByUsernameQuery query = new(user.AccountId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductsByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetUserByUsernameQuery query = new(user.AccountId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidId, result.Id);
	}
}
