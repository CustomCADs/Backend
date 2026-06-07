namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create;

using static Data.Products.TestData;

public class InvalidData : TheoryData<string, string, decimal>
{
	public InvalidData()
	{
		// Name
		Add(MinInvalidName, MinValidDescription, MinValidPrice);
		Add(MaxInvalidName, MaxValidDescription, MaxValidPrice);

		// Description
		Add(MinValidName, MinInvalidDescription, MinValidPrice);
		Add(MaxValidName, MaxInvalidDescription, MaxValidPrice);

		// Price
		Add(MinValidName, MinValidDescription, MinInvalidPrice);
		Add(MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
