namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public class InvalidCityData : Theory
{
	public InvalidCityData()
	{
		Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", string.Empty, "Flora", null, "customcads@gmail.com");
		Add("payment-method-id-2", 5, "shipment-service-2", "Romania", null!, "Brailles", "+359359359359", null);
	}
}
