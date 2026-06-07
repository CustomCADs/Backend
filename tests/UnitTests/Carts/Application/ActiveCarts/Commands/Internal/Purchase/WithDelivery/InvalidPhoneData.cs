namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public class InvalidPhoneData : Theory
{
	public InvalidPhoneData()
	{
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Slivnitsa", "Sofia", "0359359359", "customcads@gmail.com");
		Add("payment-method-id-2", "shipment-service-2", "Romania", "Brailles", "Bucharest", "+359 359 359 359", null);
	}
}
