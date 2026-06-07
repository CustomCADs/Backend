namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.TrackViewedProducts;

public class ValidData : TheoryData<bool>
{
	public ValidData()
	{
		Add(true);
		Add(false);
	}
}
