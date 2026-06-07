namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class TestData : TheoryData<decimal>
{
	public TestData()
	{
		Add(MinInvalidVolume);
		Add(MaxInvalidVolume);
	}
}
