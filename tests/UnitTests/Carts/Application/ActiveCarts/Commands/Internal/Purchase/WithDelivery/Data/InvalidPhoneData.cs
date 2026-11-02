namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class InvalidPhoneData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", "shipment-service-1", "0359359359", "gmail@customcads.com");
		yield return new("payment-method-id-2", "shipment-service-2", "+359 359 359 359", string.Empty);
	}
}
