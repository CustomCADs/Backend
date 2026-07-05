using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles.Created;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RoleCreatedHandler handler;
	private readonly RoleCreatedApplicationEvent request = new(
		Name: ValidRole,
		Description: string.Empty
	);

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
			x => x.CreateAsync(ValidRole),
			Times.Once()
		);
	}
}