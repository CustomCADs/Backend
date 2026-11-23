using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Data.IdempotencyKeys;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static IdempotencyKey CreateIdempotencyKey(
		IdempotencyKeyId? id = null,
		string? hash = null
	) => IdempotencyKey.Create(
			id: id ?? ValidId,
			hash: hash ?? ValidRequestHash
		);
}
