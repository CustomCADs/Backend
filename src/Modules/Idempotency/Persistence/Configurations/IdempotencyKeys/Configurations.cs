using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Idempotency.Persistence.Configurations.IdempotencyKeys;

internal class Configurations : IEntityTypeConfiguration<IdempotencyKey>
{
	public void Configure(EntityTypeBuilder<IdempotencyKey> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetIndexes()
			.SetValidations();
}
