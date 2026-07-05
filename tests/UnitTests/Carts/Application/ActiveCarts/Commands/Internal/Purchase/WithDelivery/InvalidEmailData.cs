namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidEmailData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "@gmail.com");
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads_gmail.com");
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@");
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail_com");
	}
}
