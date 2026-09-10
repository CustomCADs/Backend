using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Customizations.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Printing.Persistence.Configurations.Customizations;

internal static class Utilities
{
	extension(EntityTypeBuilder<Customization> builder)
	{
		internal EntityTypeBuilder<Customization> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Customization> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => CustomizationId.New(x)
				);

			builder.Property(x => x.MaterialId)
				.HasConversion(
					x => x.Value,
					x => MaterialId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Customization> SetValidations()
		{
			builder.Property(x => x.Infill)
				.IsRequired()
				.HasPrecision(4, 2)
				.HasColumnName(nameof(Customization.Infill))
				.HasComment("Floating number representing a percentage");

			builder.ComplexProperty(x => x.Volume, builder =>
			{
				builder.Property(x => x.Value)
					.IsRequired()
					.HasPrecision(28, 17)
					.HasColumnName(nameof(Volume))
					.HasComment("Arbitrary value without measuring unit");

				builder.Property(x => x.Base)
					.IsRequired()
					.HasPrecision(28, 17)
					.HasColumnName(nameof(Volume.Base))
					.HasComment("Normalization factor to help convert the Volume into mm³");

				builder.Property(x => x.Scale)
					.IsRequired()
					.HasPrecision(28, 17)
					.HasColumnName(nameof(Volume.Scale))
					.HasComment("Floating number representing a percentage");
			});

			builder.Property(x => x.Color)
				.IsRequired()
				.HasMaxLength(7)
				.HasColumnName(nameof(Customization.Color))
				.HasComment("Hexadecimal value of color");

			builder.Property(x => x.MaterialId)
				.IsRequired()
				.HasColumnName(nameof(Customization.MaterialId));

			return builder;
		}
	}

}
