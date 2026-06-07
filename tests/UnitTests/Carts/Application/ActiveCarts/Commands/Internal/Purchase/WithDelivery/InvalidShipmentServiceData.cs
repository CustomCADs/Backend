namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidShipmentServiceData : Theory
{
	public InvalidShipmentServiceData()
	{
		Add("payment-method-id-1", string.Empty, "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		Add("payment-method-id-2", null!, "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
