namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name;

using static Data.Materials.TestData;

public class TestData : TheoryData<string>
{
	public TestData()
	{
		Add(InvalidName);
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
