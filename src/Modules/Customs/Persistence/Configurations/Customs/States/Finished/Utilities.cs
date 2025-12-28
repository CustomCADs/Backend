using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs.States.Finished;

internal static class Utilities
{
	extension(EntityTypeBuilder<FinishedCustom> builder)
	{
		internal EntityTypeBuilder<FinishedCustom> SetPrimaryKey()
		{
			builder.HasKey(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<FinishedCustom> SetStronglyTypedIds()
		{
			builder.Property(x => x.CustomId)
				.HasConversion(
					x => x.Value,
					x => CustomId.New(x)
				);

			builder.Property(x => x.CadId)
				.HasConversion(
					x => x.Value,
					x => CadId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<FinishedCustom> SetNavigations()
		{
			builder
				.HasOne<Custom>()
				.WithOne(x => x.FinishedCustom)
				.HasForeignKey<FinishedCustom>(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<FinishedCustom> SetValidations()
		{
			builder.Property(x => x.Price)
				.IsRequired()
				.HasPrecision(19, 2)
				.HasColumnName(nameof(FinishedCustom.Price));

			builder.Property(x => x.FinishedAt)
				.IsRequired()
				.HasColumnName(nameof(FinishedCustom.FinishedAt));

			builder.Property(x => x.CadId)
				.IsRequired()
				.HasColumnName(nameof(FinishedCustom.CadId));

			return builder;
		}
	}

}
