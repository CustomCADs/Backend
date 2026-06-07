namespace CustomCADs.UnitTests.Printing.Domain.Materials.Create;

using static Data.Materials.TestData;

public class TestData : TheoryData<string, decimal, decimal>
{
	public TestData()
	{
		// Name
		Add(InvalidName, MaxValidDensity, MaxValidCost);
		Add(MinInvalidName, MinValidDensity, MinValidCost);
		Add(MaxInvalidName, MaxValidDensity, MaxValidCost);

		// Density
		Add(MinValidName, MinInvalidDensity, MinValidCost);
		Add(MaxValidName, MaxInvalidDensity, MaxValidCost);

		// Cost
		Add(MinValidName, MinValidDensity, MinInvalidCost);
		Add(MaxValidName, MaxValidDensity, MaxInvalidCost);
	}
}
