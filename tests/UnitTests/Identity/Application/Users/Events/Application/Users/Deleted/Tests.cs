using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users.Deleted;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly AccountDeletedHandler handler;
	private readonly AccountDeletedApplicationEvent request = new(ValidAccountId);

	private readonly Mock<IUserService> service = new();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		service.Verify(
			x => x.DeleteAsync(ValidAccountId),
			Times.Once()
		);
	}
}
