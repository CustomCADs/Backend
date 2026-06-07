namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

using static Data.Categories.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidDescription);
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
