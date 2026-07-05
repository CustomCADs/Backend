using CustomCADs.Modules.Identity.Domain.Users.Entities;
using static CustomCADs.Modules.Identity.Domain.Constants;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.AddRefreshToken;

public class Tests : Data.Users.BaseUnitTests
{
	private const string Value = "refresh-token";
	private readonly User user = CreateUser();

	[Test]
	public void AddRefreshToken_ShouldNotThrowException()
	{
		user.AddRefreshToken(Value, new(), longerSession: false);
	}

	[Test]
	[Arguments(false)]
	[Arguments(true)]
	public async Task AddRefreshToken_ShouldReturnResult(bool longerSession)
	{
		int expectedDurationDays = longerSession
			? Tokens.LongerRtDurationInDays
			: Tokens.RtDurationInDays;
		TimeSpan expectedDuration = TimeSpan.FromDays(expectedDurationDays);

		RefreshToken rt = user.AddRefreshToken(Value, new(), longerSession);
		TimeSpan actualDuration = rt.ExpiresAt - rt.IssuedAt;

		using (Assert.Multiple())
		{
			await Assert.That(rt.Value).IsEqualTo(Value);
			await Assert.That(rt.UserId).IsEqualTo(user.Id);
			await Assert.That(actualDuration).IsEqualTo(expectedDuration);
		}
	}

	[Test]
	public async Task AddRefreshToken_PopulatesProperty()
	{
		RefreshToken rt = user.AddRefreshToken(Value, new(), longerSession: false);
		await Assert.That(user.RefreshTokens).Contains(rt);
	}
}
