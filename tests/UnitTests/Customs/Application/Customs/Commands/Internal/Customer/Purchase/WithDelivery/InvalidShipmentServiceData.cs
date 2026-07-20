namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public class InvalidShipmentServiceData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", 2, string.Empty, "Bulgaria", "Sofia", "Flora", null, "customcads@gmail.com");
		yield return new("payment-method-id-2", 5, null!, "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
