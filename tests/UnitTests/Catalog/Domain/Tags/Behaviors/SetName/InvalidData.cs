namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

using static Data.Tags.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
