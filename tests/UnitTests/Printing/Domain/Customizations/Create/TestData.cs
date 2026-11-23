namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create;

using static Data.Customizations.TestData;

public class TestData : ITheoryData<(decimal, decimal, decimal, decimal, string)>
{
	public static IEnumerable<(decimal, decimal, decimal, decimal, string)> GetTestData()
	{
		// Scale
		yield return (MaxInvalidScale, MaxValidInfill, MaxValidBase, MaxValidVolume, ValidColor);
		yield return (MinInvalidScale, MinValidInfill, MinValidBase, MinValidVolume, ValidColor);

		// Infill
		yield return (MaxValidScale, MaxInvalidInfill, MaxValidBase, MaxValidVolume, ValidColor);
		yield return (MinValidScale, MinInvalidInfill, MinValidBase, MinValidVolume, ValidColor);

		// Base
		yield return (MaxValidScale, MaxValidInfill, MaxInvalidBase, MaxValidVolume, ValidColor);
		yield return (MinValidScale, MinValidInfill, MinInvalidBase, MinValidVolume, ValidColor);

		// Volume
		yield return (MaxValidScale, MaxValidInfill, MaxValidBase, MaxInvalidVolume, ValidColor);
		yield return (MinValidScale, MinValidInfill, MinValidBase, MinInvalidVolume, ValidColor);

		// Color
		yield return (MaxValidScale, MaxValidInfill, MaxValidBase, MaxValidVolume, InvalidColor);
		yield return (MinValidScale, MinValidInfill, MinValidBase, MinValidVolume, MaxInvalidColor);
		yield return (MaxValidScale, MaxValidInfill, MaxValidBase, MaxValidVolume, MinInvalidColor);
	}
}
