namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class ValidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Flora 66", "+359359359359", "customcads@gmail.com");
		yield return new("payment-method-id-2", "shipment-service-2", "Romania", "Bucharest", "Brailles", "+404040404040", "customcadsolutions@gmail.com");
	}
}
