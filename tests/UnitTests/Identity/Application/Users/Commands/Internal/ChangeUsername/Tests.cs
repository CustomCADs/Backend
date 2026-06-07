using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ChangeUsername;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly ChangeUsernameHandler handler;
	private readonly ChangeUsernameCommand request = new(
		Id: ValidAccountId,
		Username: MinValidUsername,
		FirstName: null,
		LastName: null
	);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public Tests()
	{
		handler = new(service.Object, raiser.Object);

		service.Setup(x => x.GetByAccountIdAsync(ValidAccountId))
			.ReturnsAsync(CreateUser());
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByAccountIdAsync(ValidAccountId),
			Times.Once()
		);
		service.Verify(
			x => x.UpdateUsernameAsync(ValidId, MinValidUsername),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<UserEditedApplicationEvent>(x =>
					x.Username == MinValidUsername
					&& x.Id == ValidAccountId
				)
			),
			Times.Once()
		);
	}
}
