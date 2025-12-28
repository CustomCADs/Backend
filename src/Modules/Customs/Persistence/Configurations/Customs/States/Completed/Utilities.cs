using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs.States.Completed;

internal static class Utilities
{
	extension(EntityTypeBuilder<CompletedCustom> builder)
	{
		internal EntityTypeBuilder<CompletedCustom> SetPrimaryKey()
		{
			builder.HasKey(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<CompletedCustom> SetStronglyTypedIds()
		{
			builder.Property(x => x.CustomId)
				.HasConversion(
					x => x.Value,
					x => CustomId.New(x)
				);

			builder.Property(x => x.ShipmentId)
				.HasConversion(
					x => ShipmentId.Unwrap(x),
					x => ShipmentId.New(x)
				);

			builder.Property(x => x.CustomizationId)
				.HasConversion(
					x => CustomizationId.Unwrap(x),
					x => CustomizationId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<CompletedCustom> SetNavigations()
		{
			builder
				.HasOne<Custom>()
				.WithOne(x => x.CompletedCustom)
				.HasForeignKey<CompletedCustom>(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<CompletedCustom> SetValidations()
		{
			builder.Property(x => x.PaymentStatus)
				.IsRequired()
				.HasColumnName(nameof(CompletedCustom.PaymentStatus))
				.HasConversion(
					x => x.ToString(),
					x => Enum.Parse<PaymentStatus>(x)
				);

			builder.Property(x => x.ShipmentId)
				.HasColumnName(nameof(CompletedCustom.ShipmentId));

			builder.Property(x => x.CustomizationId)
				.HasColumnName(nameof(CompletedCustom.CustomizationId));

			return builder;
		}
	}

}
