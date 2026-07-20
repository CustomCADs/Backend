using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Delete;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly DeleteUserHandler handler;
	private readonly DeleteUserCommand request = new(ValidAccountId);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public Tests()
	{
		handler = new(service.Object, raiser.Object);
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.DeleteAsync(ValidAccountId),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<UserDeletedApplicationEvent>(x => x.Id == ValidAccountId)
			),
			Times.Once()
		);
	}
}