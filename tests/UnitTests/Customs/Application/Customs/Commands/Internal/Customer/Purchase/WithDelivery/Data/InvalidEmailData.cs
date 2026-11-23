namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;

public class InvalidEmailData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", 2, "shipment-service-1", string.Empty, "@gmail.com");
		yield return new("payment-method-id-1", 5, "shipment-service-1", string.Empty, "customcads_gmail.com");
		yield return new("payment-method-id-1", 2, "shipment-service-1", string.Empty, "customcads@");
		yield return new("payment-method-id-1", 5, "shipment-service-1", string.Empty, "customcads@gmail_com");
	}
}
