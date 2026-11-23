namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Behaviors.Activate;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return string.Empty;
		yield return null!;
	}
}
