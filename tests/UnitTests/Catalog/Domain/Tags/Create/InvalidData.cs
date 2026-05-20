namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create;

using static Data.Tags.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
