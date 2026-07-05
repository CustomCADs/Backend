using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Queries.Internal.GetByUsername;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly GetUserByUsernameHandler handler;
	private readonly GetUserByUsernameQuery request = new(ValidAccountId, "refresh-token");

	private readonly Mock<IUserService> service = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly User user = CreateUser();

	public Tests()
	{
		user.AddRefreshToken(request.RefreshToken!, ValidFingerprint, false);

		handler = new(service.Object, sender.Object);

		service.Setup(x => x.GetByAccountIdAsync(user.AccountId))
			.ReturnsAsync(user);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == user.Username),
			ct
		)).ReturnsAsync(new AccountInfoDto(ValidAccountId, DateTimeOffset.UtcNow, true, null, null));
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByAccountIdAsync(user.AccountId),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == user.Username),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountViewedProductsByUsernameQuery>(x => x.Username == user.Username),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Id).IsEqualTo(ValidId);
	}
}