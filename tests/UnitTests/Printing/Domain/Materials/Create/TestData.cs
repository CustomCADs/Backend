namespace CustomCADs.UnitTests.Printing.Domain.Materials.Create;

using static Data.Materials.TestData;

public class TestData : TheoryData<(string, decimal, decimal)>
{
	public static IEnumerable<(string, decimal, decimal)> GetTestData()
	{
		// Name
		yield return (InvalidName, MaxValidDensity, MaxValidCost);
		yield return (MinInvalidName, MinValidDensity, MinValidCost);
		yield return (MaxInvalidName, MaxValidDensity, MaxValidCost);

		// Density
		yield return (MinValidName, MinInvalidDensity, MinValidCost);
		yield return (MaxValidName, MaxInvalidDensity, MaxValidCost);

		// Cost
		yield return (MinValidName, MinValidDensity, MinInvalidCost);
		yield return (MaxValidName, MaxValidDensity, MaxInvalidCost);
	}
}
