using CustomCADs.Modules.Identity.Domain.Users.Entities;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.RemoveRefreshToken;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly User user = CreateUser();
	private readonly RefreshToken rt;

	public Tests()
	{
		rt = user.AddRefreshToken("refresh-token", new(), longerSession: false);
	}

	[Test]
	public void RemoveRefreshToken_ShouldNotThrowException()
	{
		user.RemoveRefreshToken(rt);
	}

	[Test]
	public async Task RemoveRefreshToken_ShouldReturnResult()
	{
		bool firstResult = user.RemoveRefreshToken(rt);
		bool secondResult = user.RemoveRefreshToken(rt);

		using (Assert.Multiple())
		{
			await Assert.That(firstResult).IsTrue();
			await Assert.That(secondResult).IsFalse();
		}
	}

	[Test]
	public async Task RemoveRefreshToken_PopulatesProperty()
	{
		user.RemoveRefreshToken(rt);
		await Assert.That(user.RefreshTokens).DoesNotContain(rt);
	}
}
