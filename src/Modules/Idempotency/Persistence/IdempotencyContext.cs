using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Idempotency.Persistence;

using static PersistenceConstants;

public class IdempotencyContext(DbContextOptions<IdempotencyContext> opt) : DbContext(opt)
{
	public required DbSet<IdempotencyKey> IdempotencyKeys { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Idempotency);
		builder.ApplyConfigurationsFromAssembly(IdempotencyPersistenceReference.Assembly);
	}
}
