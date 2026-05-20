using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users.Deleted;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly AccountDeletedHandler handler;
	private readonly Mock<IUserService> service = new();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		AccountDeletedApplicationEvent @event = new(ValidAccountId);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		service.Verify(
			x => x.DeleteAsync(ValidAccountId),
			Times.Once()
		);
	}
}
