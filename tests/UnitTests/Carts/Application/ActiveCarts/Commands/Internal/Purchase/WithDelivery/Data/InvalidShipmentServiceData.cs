namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class InvalidShipmentServiceData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", string.Empty, string.Empty, "customcads@gmail.com");
		yield return new("payment-method-id-2", null!, "+359359359359", string.Empty);
	}
}
