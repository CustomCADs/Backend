namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
