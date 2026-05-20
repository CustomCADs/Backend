namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Color;

using static Data.Customizations.TestData;

public class TestData : TheoryData<string>
{
	public TestData()
	{
		Add(InvalidColor);
		Add(MinInvalidColor);
		Add(MaxInvalidColor);
	}
}
