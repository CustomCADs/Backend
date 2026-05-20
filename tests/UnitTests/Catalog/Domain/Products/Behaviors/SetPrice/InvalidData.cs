namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

using static Data.Products.TestData;

public class InvalidData : TheoryData<decimal>
{
	public InvalidData()
	{
		Add(MinInvalidPrice);
		Add(MaxInvalidPrice);
	}
}
