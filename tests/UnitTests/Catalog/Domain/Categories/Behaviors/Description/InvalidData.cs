namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

using static Data.Categories.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidDescription);
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
