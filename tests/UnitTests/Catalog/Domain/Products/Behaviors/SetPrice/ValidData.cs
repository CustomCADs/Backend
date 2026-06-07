namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

using static Data.Products.TestData;

public class ValidData : TheoryData<decimal>
{
	public ValidData()
	{
		Add(MinValidPrice);
		Add(MaxValidPrice);
	}
}
