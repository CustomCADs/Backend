namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class InvalidEmailData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", string.Empty, "@gmail.com");
		yield return new("payment-method-id-1", "shipment-service-1", string.Empty, "customcads_gmail.com");
		yield return new("payment-method-id-1", "shipment-service-1", string.Empty, "customcads@");
		yield return new("payment-method-id-1", "shipment-service-1", string.Empty, "customcads@gmail_com");
	}
}
