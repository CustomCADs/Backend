namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create;

using static Data.Shipments.TestData;

public class InvalidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		// Service
		yield return new(InvalidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight);

		// Email
		yield return new(ValidService, InvalidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxValidWeight);

		// Phone
		yield return new(ValidService, ValidEmail, InvalidPhone, ValidRecipient, MaxValidCount, MaxValidWeight);

		// Recipient
		yield return new(ValidService, ValidEmail, ValidPhone, InvalidRecipient, MaxValidCount, MaxValidWeight);

		// Count
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxInvalidCount, MaxValidWeight);
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MinInvalidCount, MaxValidWeight);

		// Weight
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MaxInvalidWeight);
		yield return new(ValidService, ValidEmail, ValidPhone, ValidRecipient, MaxValidCount, MinInvalidWeight);
	}
}
