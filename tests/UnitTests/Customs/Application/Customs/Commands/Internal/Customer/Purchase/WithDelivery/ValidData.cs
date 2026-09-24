namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public class ValidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", 2, "shipment-service-1", "+359359359359", "customcads@gmail.com");
		yield return new("payment-method-id-2", 5, "shipment-service-2", "+404040404040", "customcadsolutions@gmail.com");
	}
}
