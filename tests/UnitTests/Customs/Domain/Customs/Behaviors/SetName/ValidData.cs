namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
