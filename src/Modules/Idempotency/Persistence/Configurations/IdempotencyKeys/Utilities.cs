using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Idempotency.Persistence.Configurations.IdempotencyKeys;

internal static class Utilities
{
	extension(EntityTypeBuilder<IdempotencyKey> builder)
	{
		internal EntityTypeBuilder<IdempotencyKey> SetPrimaryKey()
		{
			builder.HasKey(x => new { x.Id, x.RequestHash });

			return builder;
		}

		internal EntityTypeBuilder<IdempotencyKey> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.HasConversion(
					x => x.Value,
					x => IdempotencyKeyId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<IdempotencyKey> SetIndexes()
		{
			builder.HasIndex(x => x.CreatedAt);

			return builder;
		}

		internal EntityTypeBuilder<IdempotencyKey> SetValidations()
		{
			builder.Property(x => x.RequestHash)
				.IsRequired()
				.HasColumnName(nameof(IdempotencyKey.RequestHash));

			builder.Property(x => x.ResponseBody)
				.HasColumnName(nameof(IdempotencyKey.ResponseBody));

			builder.Property(x => x.StatusCode)
				.HasColumnName(nameof(IdempotencyKey.StatusCode));

			builder.Property(x => x.CreatedAt)
				.IsRequired()
				.HasColumnName(nameof(IdempotencyKey.CreatedAt));

			return builder;
		}
	}

}
