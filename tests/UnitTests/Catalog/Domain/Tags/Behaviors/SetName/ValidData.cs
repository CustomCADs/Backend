namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

using static Data.Tags.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
