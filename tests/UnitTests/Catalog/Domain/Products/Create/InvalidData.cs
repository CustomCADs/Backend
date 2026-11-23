namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create;

using static Data.Products.TestData;

public class InvalidData : ITheoryData<(string, string, decimal)>
{
	public static IEnumerable<(string, string, decimal)> GetTestData()
	{
		// Name
		yield return (MinInvalidName, MinValidDescription, MinValidPrice);
		yield return (MaxInvalidName, MaxValidDescription, MaxValidPrice);

		// Description
		yield return (MinValidName, MinInvalidDescription, MinValidPrice);
		yield return (MaxValidName, MaxInvalidDescription, MaxValidPrice);

		// Price
		yield return (MinValidName, MinValidDescription, MinInvalidPrice);
		yield return (MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
