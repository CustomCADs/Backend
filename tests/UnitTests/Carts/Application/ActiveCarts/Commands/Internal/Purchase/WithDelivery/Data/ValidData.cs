namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class ValidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "+359359359359", "customcads@gmail.com");
		yield return new("payment-method-id-2", "shipment-service-2", "+404040404040", "customcadsolutions@gmail.com");
	}
}
