namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

using static Data.Products.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
