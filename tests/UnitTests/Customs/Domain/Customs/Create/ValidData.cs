namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create;

using static Data.Customs.TestData;

public class ValidData : TheoryData<string, string, bool>
{
	public ValidData()
	{
		Add(MinValidName, MinValidDescription, true);
		Add(MaxValidName, MaxValidDescription, false);
	}
}
