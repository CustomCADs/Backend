using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create;

using static Data.IdempotencyKeys.TestData;

public class InvalidData : ITheoryData<(IdempotencyKeyId, string)>
{
	public static IEnumerable<(IdempotencyKeyId, string)> GetTestData()
	{
		// Id
		yield return (InvalidId, ValidRequestHash);

		// RequestHash
		yield return (ValidId, InvalidRequestHash);
	}
}
