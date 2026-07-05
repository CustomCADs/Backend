namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create;

using static Data.Shipments.TestData;

public class InvalidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		// Service
		yield return new(InvalidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Email
		yield return new(ValidService, InvalidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Phone
		yield return new(ValidService, ValidEmail, InvalidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Recipient
		yield return new(ValidService, ValidEmail, ValidPhone, InvalidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Count
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxInvalidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MinInvalidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Weight
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxInvalidWeight, ValidCountry, ValidCity, ValidStreet);
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MinInvalidWeight, ValidCountry, ValidCity, ValidStreet);

		// Country
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, InvalidCountry, ValidCity, ValidStreet);

		// City
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, InvalidCity, ValidStreet);

		// Street
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, InvalidStreet);
	}
}
