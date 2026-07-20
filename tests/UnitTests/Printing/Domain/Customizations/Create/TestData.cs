namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create;

using static Data.Customizations.TestData;

public class TestData : ITheoryData<(decimal, decimal, decimal, string)>
{
	public static IEnumerable<(decimal, decimal, decimal, string)> GetTestData()
	{
		// Scale
		yield return (MaxInvalidScale, MaxValidInfill, MaxValidVolume, ValidColor);
		yield return (MinInvalidScale, MinValidInfill, MinValidVolume, ValidColor);

		// Infill
		yield return (MaxValidScale, MaxInvalidInfill, MaxValidVolume, ValidColor);
		yield return (MinValidScale, MinInvalidInfill, MinValidVolume, ValidColor);

		// Volume
		yield return (MaxValidScale, MaxValidInfill, MaxInvalidVolume, ValidColor);
		yield return (MinValidScale, MinValidInfill, MinInvalidVolume, ValidColor);

		// Color
		yield return (MaxValidScale, MaxValidInfill, MaxValidVolume, InvalidColor);
		yield return (MinValidScale, MinValidInfill, MinValidVolume, MaxInvalidColor);
		yield return (MaxValidScale, MaxValidInfill, MaxValidVolume, MinInvalidColor);
	}
}
