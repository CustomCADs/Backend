using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Delete;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly DeleteUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public Tests()
	{
		handler = new(service.Object, raiser.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		DeleteUserCommand command = new(user.AccountId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(
			x => x.DeleteAsync(user.AccountId),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		DeleteUserCommand command = new(user.AccountId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<UserDeletedApplicationEvent>(x => x.Id == user.AccountId)
			),
			Times.Once()
		);
	}
}
