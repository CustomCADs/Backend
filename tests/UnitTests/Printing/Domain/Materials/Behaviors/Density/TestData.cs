namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density;

using static Data.Materials.TestData;

public class TestData : TheoryData<decimal>
{
	public TestData()
	{
		Add(MinInvalidDensity);
		Add(MaxInvalidDensity);
	}
}
