namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using static Data.Customs.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
