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
	private static readonly AccountInfoDto Info = new(
		Id: ValidAccountId,
		CreatedAt: DateTimeOffset.UtcNow,
		TrackViewedProducts: true,
		FirstName: null,
		LastName: null
	);
	private static readonly ViewedProductDto[] ViewedProducts = [];

	public Tests()
	{
		user.AddRefreshToken("random-value1", ValidFingerprint, false);
		user.AddRefreshToken("random-value2", ValidFingerprint, false);
		user.AddRefreshToken("random-value3", ValidFingerprint, false);
		user.AddRefreshToken(request.RefreshToken!, ValidFingerprint, false);

		handler = new(service.Object, sender.Object);

		service.Setup(x => x.GetByAccountIdAsync(user.AccountId))
			.ReturnsAsync(user);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == user.Username),
			ct
		)).ReturnsAsync(Info);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductsByUsernameQuery>(x => x.Username == user.Username),
			ct
		)).ReturnsAsync(ViewedProducts);
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
		using (Assert.Multiple())
		{
			// user info:
			await Assert.That(result.Id).IsEqualTo(ValidId);
			await Assert.That(result.Role).IsEqualTo(user.Role);
			await Assert.That(result.Username).IsEqualTo(user.Username);
			await Assert.That(result.Email).IsEqualTo(user.Email);

			// account info:
			await Assert.That(result.TrackViewedProducts).IsEqualTo(Info.TrackViewedProducts);
			await Assert.That(result.CreatedAt).IsEqualTo(Info.CreatedAt);
			await Assert.That(result.FirstName).IsEqualTo(Info.FirstName);
			await Assert.That(result.LastName).IsEqualTo(Info.LastName);

			// collections:
			await Assert.That(result.ViewedProducts).IsEquivalentTo(ViewedProducts);
			await Assert.That(result.Fingerprints.Select(x => x.IssuedAt)).IsEquivalentTo(user.RefreshTokens.OrderByDescending(x => x.IssuedAt).Select(x => x.IssuedAt));
		}
	}
}
