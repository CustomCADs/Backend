
namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.TrackViewedProducts;

public class Tests : Data.Accounts.BaseUnitTests
{
	public static IEnumerable<bool> GetTestData() => [true, false];

	[Test]
	[MethodDataSource(nameof(GetTestData))]
	public void SetTrackViewedProducts_ShouldNotThrowException(bool value)
	{
		var account = CreateAccount();

		account.SetTrackViewedProducts(value);
	}

	[Test]
	[MethodDataSource(nameof(GetTestData))]
	public async Task SetTrackViewedProducts_SetsTrackViewedProducts(bool value)
	{
		var account = CreateAccount();

		account.SetTrackViewedProducts(value);

		await Assert.That(account.TrackViewedProducts).IsEqualTo(value);
	}
}
