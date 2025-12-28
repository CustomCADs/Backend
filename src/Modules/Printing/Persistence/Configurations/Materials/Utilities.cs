using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Printing.Persistence.Configurations.Materials;

using static DomainConstants.Textures;
using static MaterialConstants;

internal static class Utilities
{
	extension(EntityTypeBuilder<Material> builder)
	{
		internal EntityTypeBuilder<Material> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Material> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.HasConversion(
					x => x.Value,
					x => MaterialId.New(x)
				)
				.UseIdentityColumn();

			builder.Property(x => x.TextureId)
				.HasConversion(
					x => x.Value,
					x => ImageId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Material> SetValidations()
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Material.Name));

			builder.Property(x => x.Density)
				.IsRequired()
				.HasPrecision(18, 2)
				.HasColumnName(nameof(Material.Density))
				.HasComment("Measured in g/cm³");

			builder.Property(x => x.Cost)
				.IsRequired()
				.HasPrecision(18, 2)
				.HasColumnName(nameof(Material.Cost))
				.HasComment("Measured in EUR/kg");

			builder.Property(x => x.TextureId)
				.IsRequired()
				.HasColumnName(nameof(Material.TextureId));

			return builder;
		}

		internal EntityTypeBuilder<Material> SetSeeding()
		{
			builder.HasData([
				Material.CreateWithId(MaterialId.New(1), "PLA", 1.24m, 30m, PLA),
				Material.CreateWithId(MaterialId.New(2), "ABS", 1.04m, 30m, ABS),
				Material.CreateWithId(MaterialId.New(3), "Glow in dark", 1.25m, 30m, GlowInDark),
				Material.CreateWithId(MaterialId.New(4), "TUF", 1.27m, 30m, TUF),
				Material.CreateWithId(MaterialId.New(5), "Wood", 1.23m, 30m, Wood),
			]);

			return builder;
		}
	}

}
