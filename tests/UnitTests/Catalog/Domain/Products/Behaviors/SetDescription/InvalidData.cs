namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

using static Data.Products.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
