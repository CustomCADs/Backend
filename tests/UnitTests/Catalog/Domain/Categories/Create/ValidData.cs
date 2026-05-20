namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create;

using static Data.Categories.TestData;

public class ValidData : TheoryData<string, string>
{
	public ValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
