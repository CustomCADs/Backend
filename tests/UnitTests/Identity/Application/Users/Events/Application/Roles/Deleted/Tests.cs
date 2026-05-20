using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles.Deleted;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RoleDeletedHandler handler;
	private readonly Mock<IRoleService> service = new();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RoleDeletedApplicationEvent @event = new(ValidRole);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		service.Verify(x => x.DeleteAsync(ValidRole), Times.Once());
	}
}
