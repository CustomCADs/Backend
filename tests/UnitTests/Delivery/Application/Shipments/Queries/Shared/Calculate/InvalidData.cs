namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Calculate;

public class InvalidData : ITheoryData<(string, string, string)>
{
	public static IEnumerable<(string, string, string)> GetTestData()
	{
		// Country
		yield return (null!, "Burgas", "Slivnitsa");
		yield return (string.Empty, "Burgas", "Slivnitsa");

		// City
		yield return ("Bulgaria", null!, "Slivnitsa");
		yield return ("Bulgaria", string.Empty, "Slivnitsa");

		// Street
		yield return ("Bulgaria", "Burgas", null!);
		yield return ("Bulgaria", "Burgas", string.Empty);
	}
}
