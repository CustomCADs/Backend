using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles.Deleted;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RoleDeletedHandler handler;
	private readonly RoleDeletedApplicationEvent request = new(ValidRole);

	private readonly Mock<IRoleService> service = new();

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
			x => x.DeleteAsync(ValidRole),
			Times.Once()
		);
	}
}