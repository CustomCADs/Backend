using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Modules.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Idempotency.Persistence.Repositories.IdempotencyKeys;

public class Reads(IdempotencyContext context) : IIdempotencyKeyReads
{
	public async Task<IdempotencyKey?> SingleByIdAsync(IdempotencyKeyId id, string requestHash, bool track = true, CancellationToken ct = default)
		=> await context.IdempotencyKeys
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id && x.RequestHash == requestHash, ct)
			.ConfigureAwait(false);
}
