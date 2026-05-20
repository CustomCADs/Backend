namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

using static Data.Categories.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidName);
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
