using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create;

using static Data.IdempotencyKeys.TestData;

public class InvalidData : TheoryData<IdempotencyKeyId, string>
{
	public InvalidData()
	{
		// Id
		Add(InvalidId, ValidRequestHash);

		// RequestHash
		Add(ValidId, InvalidRequestHash);
	}
}
