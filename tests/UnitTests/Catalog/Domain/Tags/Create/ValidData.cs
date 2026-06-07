namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create;

using static Data.Tags.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
