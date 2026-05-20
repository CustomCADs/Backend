namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

using static Data.Products.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
