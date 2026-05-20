namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Calculate;

public class InvalidData : TheoryData<string, string, string>
{
	public InvalidData()
	{
		// Country
		Add(null!, "Burgas", "Slivnitsa");
		Add(string.Empty, "Burgas", "Slivnitsa");

		// City
		Add("Bulgaria", null!, "Slivnitsa");
		Add("Bulgaria", string.Empty, "Slivnitsa");

		// Street
		Add("Bulgaria", "Burgas", null!);
		Add("Bulgaria", "Burgas", string.Empty);
	}
}
