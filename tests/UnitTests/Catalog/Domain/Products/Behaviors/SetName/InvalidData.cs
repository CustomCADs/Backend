namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

using static Data.Products.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
