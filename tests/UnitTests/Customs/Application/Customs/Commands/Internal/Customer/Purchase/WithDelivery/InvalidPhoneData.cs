namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public class InvalidPhoneData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		yield return new("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofi a", "Flora", "0359359359", "customcads@gmail.com");
		yield return new("payment-method-id-2", 5, "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359 359 359 359", null);
	}
}
