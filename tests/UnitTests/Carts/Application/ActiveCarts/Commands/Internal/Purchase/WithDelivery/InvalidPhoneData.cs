namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidPhoneData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "Bulgaria", "Slivnitsa", "Sofia", "0359359359", "customcads@gmail.com");
		yield return new("payment-method-id-2", "shipment-service-2", "Romania", "Brailles", "Bucharest", "+359 359 359 359", null);
	}
}
