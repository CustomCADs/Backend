namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create;

using static Data.Products.TestData;

public class ValidData : TheoryData<string, string, decimal>
{
	public ValidData()
	{
		Add(MinValidName, MinValidDescription, MinValidPrice);
		Add(MaxValidName, MaxValidDescription, MaxValidPrice);
	}
}
