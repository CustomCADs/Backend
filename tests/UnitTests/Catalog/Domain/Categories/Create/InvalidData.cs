namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create;

using static Data.Categories.TestData;

public class InvalidData : TheoryData<string, string>
{
	public InvalidData()
	{
		// Name
		Add(InvalidName, ValidDescription);
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);

		// Description
		Add(ValidName, InvalidDescription);
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
