namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class TestData : ITheoryData<(decimal Value, decimal Base, decimal Scale)>
{
	public static IEnumerable<(decimal Value, decimal Base, decimal Scale)> GetTestData()
	{
		yield return (MinInvalidVolume, MinValidBase, MinValidScale);
		yield return (MinValidVolume, MaxInvalidBase, MaxValidScale);
		yield return (MaxValidVolume, MaxValidBase, MinInvalidScale);
	}
}
