namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Infill;

using static Data.Customizations.TestData;

public class TestData : TheoryData<decimal>
{
	public TestData()
	{
		Add(MinInvalidInfill);
		Add(MaxInvalidInfill);
	}
}
