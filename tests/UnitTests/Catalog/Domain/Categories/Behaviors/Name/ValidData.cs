namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

using static Data.Categories.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
