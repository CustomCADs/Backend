namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidShipmentServiceData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", string.Empty, "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		yield return new("payment-method-id-2", null!, "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
