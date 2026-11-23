namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create;

using static Data.Products.TestData;

public class ValidData : ITheoryData<(string, string, decimal)>
{
	public static IEnumerable<(string, string, decimal)> GetTestData()
	{
		yield return (MinValidName, MinValidDescription, MinValidPrice);
		yield return (MaxValidName, MaxValidDescription, MaxValidPrice);
	}
}
