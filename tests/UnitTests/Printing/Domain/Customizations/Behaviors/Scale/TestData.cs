namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Scale;

using static Data.Customizations.TestData;

public class TestData : TheoryData<decimal>
{
	public TestData()
	{
		Add(MinInvalidScale);
		Add(MaxInvalidScale);
	}
}
