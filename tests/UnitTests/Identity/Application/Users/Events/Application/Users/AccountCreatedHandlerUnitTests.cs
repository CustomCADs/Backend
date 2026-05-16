using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users;

using static UsersData;

public class AccountCreatedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly AccountCreatedHandler handler;
	private readonly Mock<IUserService> service = new();

	private readonly User user = CreateUser();

	public AccountCreatedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		AccountCreatedApplicationEvent @event = new(
			Id: user.AccountId,
			Role: user.Role,
			Username: user.Username,
			Email: user.Email.Value,
			Password: MinValidPassword
		);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		service.Verify(x => x.CreateAsync(
			It.Is<User>(x =>
				x.Role == @event.Role
				&& x.Username == @event.Username
				&& x.Email.Value == @event.Email
				&& x.AccountId == @event.Id
			),
			@event.Password
		), Times.Once());
	}
}
