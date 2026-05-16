using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users;

using static UsersData;

public class AccountDeletedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly AccountDeletedHandler handler;
	private readonly Mock<IUserService> service = new();

	public AccountDeletedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		AccountDeletedApplicationEvent @event = new(ValidAccountId);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		service.Verify(x => x.DeleteAsync(ValidAccountId), Times.Once());
	}
}
