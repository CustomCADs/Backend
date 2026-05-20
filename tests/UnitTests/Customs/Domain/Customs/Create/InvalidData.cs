namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create;

using static Data.Customs.TestData;

public class InvalidData : TheoryData<string, string, bool>
{
	public InvalidData()
	{
		// Name
		Add(MinInvalidName, MinValidDescription, true);
		Add(MaxInvalidName, MaxValidDescription, false);

		// Description
		Add(MinValidName, MinInvalidDescription, true);
		Add(MaxValidName, MaxInvalidDescription, false);
	}
}
