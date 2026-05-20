using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles.Created;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RoleCreatedHandler handler;
	private readonly Mock<IRoleService> service = new();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RoleCreatedApplicationEvent @event = new(
			Name: ValidRole,
			Description: string.Empty
		);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		service.Verify(
			x => x.CreateAsync(ValidRole),
			Times.Once()
		);
	}
}
