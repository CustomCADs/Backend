namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using static Data.Customs.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
