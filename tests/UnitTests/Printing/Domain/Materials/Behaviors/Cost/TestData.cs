namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Cost;

using static Data.Materials.TestData;

public class TestData : TheoryData<decimal>
{
	public TestData()
	{
		Add(MinInvalidCost);
		Add(MaxInvalidCost);
	}
}
