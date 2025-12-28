using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs.States.Accepted;

internal static class Utilities
{
	extension(EntityTypeBuilder<AcceptedCustom> builder)
	{
		internal EntityTypeBuilder<AcceptedCustom> SetPrimaryKey()
		{
			builder.HasKey(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<AcceptedCustom> SetStronglyTypedIds()
		{
			builder.Property(x => x.CustomId)
				.HasConversion(
					x => x.Value,
					x => CustomId.New(x)
				);

			builder.Property(x => x.DesignerId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<AcceptedCustom> SetNavigations()
		{
			builder
				.HasOne<Custom>()
				.WithOne(x => x.AcceptedCustom)
				.HasForeignKey<AcceptedCustom>(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<AcceptedCustom> SetValidations()
		{
			builder.Property(x => x.AcceptedAt)
				.IsRequired()
				.HasColumnName(nameof(AcceptedCustom.AcceptedAt));

			builder.Property(x => x.DesignerId)
				.IsRequired()
				.HasColumnName(nameof(AcceptedCustom.DesignerId));

			return builder;
		}
	}
}
