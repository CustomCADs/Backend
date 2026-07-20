using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users.Created;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly AccountCreatedHandler handler;
	private readonly AccountCreatedApplicationEvent request = new(
		Id: ValidAccountId,
		Role: ValidRole,
		Username: MinValidUsername,
		Email: ValidEmail,
		Password: MinValidPassword
	);

	private readonly Mock<IUserService> service = new();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		service.Verify(
			x => x.CreateAsync(
				It.Is<User>(x =>
					x.Role == request.Role
					&& x.Username == request.Username
					&& x.Email.Value == request.Email
					&& x.AccountId == request.Id
				),
				request.Password
			),
			Times.Once()
		);
	}
}