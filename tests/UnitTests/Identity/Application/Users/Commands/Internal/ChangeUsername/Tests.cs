using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ChangeUsername;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly ChangeUsernameHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser();

	public Tests()
	{
		handler = new(service.Object, raiser.Object);

		service.Setup(x => x.GetByAccountIdAsync(user.AccountId)).ReturnsAsync(user);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Id: user.AccountId,
			Username: MinValidUsername,
			FirstName: null,
			LastName: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByAccountIdAsync(user.AccountId), Times.Once());
		service.Verify(x => x.UpdateUsernameAsync(user.Id, MinValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Id: user.AccountId,
			Username: MinValidUsername,
			FirstName: null,
			LastName: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserEditedApplicationEvent>(x =>
				x.Username == user.Username
				&& x.Id == user.AccountId
			)
		), Times.Once());
	}
}
