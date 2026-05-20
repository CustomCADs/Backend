namespace CustomCADs.UnitTests.Files.Domain.Images.Create;

using static Data.Images.TestData;

public class InvalidData : TheoryData<string, string>
{
	public InvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType);

		// Content Type
		Add(ValidKey, InvalidContentType);
	}
}
