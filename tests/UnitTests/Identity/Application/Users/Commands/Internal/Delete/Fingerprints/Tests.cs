using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete.Fingerprints;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Delete.Fingerprints;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly DeleteFingerprintHandler handler;
	private readonly DeleteFingerprintCommand request = new(ValidRefreshTokenId, RefreshToken, ValidAccountId);

	private readonly Mock<IUserService> service = new();

	private const string RefreshToken = "refresh-token";
	private readonly User user = CreateUser();
	private readonly RefreshTokenId OldRefreshTokenId;

	public Tests()
	{
		handler = new(service.Object);

		var refreshToken = user.AddRefreshToken("old-refresh-token", new(), false);
		OldRefreshTokenId = refreshToken.Id;

		user.AddRefreshToken(RefreshToken, new(), false);

		service.Setup(x => x.GetByRefreshTokenAsync(RefreshToken))
			.ReturnsAsync((user, refreshToken));
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByRefreshTokenAsync(RefreshToken),
			Times.Once()
		);
		service.Verify(
			x => x.RevokeRefreshTokenAsync(ValidRefreshTokenId),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNoRefreshToken()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request with { CurrentRefreshToken = null }, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenRefrsehTokenIsCurrent()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomException>(() => handler.Handle(request with { RefreshTokenId = OldRefreshTokenId }, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request with { CallerId = AccountId.New() }, ct));
	}
}
