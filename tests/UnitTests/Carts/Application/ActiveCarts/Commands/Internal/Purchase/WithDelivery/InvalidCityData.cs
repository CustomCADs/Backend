namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidCityData : Theory
{
	public InvalidCityData()
	{
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", string.Empty, "Slivnitsa", null, "customcads@gmail.com");
		Add("payment-method-id-2", "shipment-service-2", "Romania", null!, "Brailles", "+359359359359", null);
	}
}
