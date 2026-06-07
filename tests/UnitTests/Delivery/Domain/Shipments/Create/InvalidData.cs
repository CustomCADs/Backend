namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create;

using static Data.Shipments.TestData;

public class InvalidData : Theory
{
	public InvalidData()
	{
		// Service
		Add(InvalidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Email
		Add(ValidService, InvalidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Phone
		Add(ValidService, ValidEmail, InvalidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Recipient
		Add(ValidService, ValidEmail, ValidPhone, InvalidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Count
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxInvalidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MinInvalidCount, MaxValidWeight, ValidCountry, ValidCity, ValidStreet);

		// Weight
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxInvalidWeight, ValidCountry, ValidCity, ValidStreet);
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MinInvalidWeight, ValidCountry, ValidCity, ValidStreet);

		// Country
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, InvalidCountry, ValidCity, ValidStreet);

		// City
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, InvalidCity, ValidStreet);

		// Street
		Add(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight, ValidCountry, ValidCity, InvalidStreet);
	}
}

public class Theory : TheoryData<
	string,
	string?,
	string?,
	string,
	int,
	double,
	string,
	string,
	string
>;
