namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public class InvalidEmailData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "@gmail.com");
		yield return new("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads_gmail.com");
		yield return new("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@");
		yield return new("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@gmail_com");
	}
}
