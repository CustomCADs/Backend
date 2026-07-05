namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class ValidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		yield return new("payment-method-id-2", "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
