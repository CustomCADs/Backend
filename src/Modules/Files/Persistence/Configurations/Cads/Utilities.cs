using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Files.Persistence.Configurations.Cads;

internal static class Utilities
{
	extension(EntityTypeBuilder<Cad> builder)
	{
		internal EntityTypeBuilder<Cad> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Cad> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => CadId.New(x)
				);

			builder.Property(x => x.OwnerId)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Cad> SetValueObjects()
		{
			builder.ComplexProperty(x => x.CamCoordinates, x =>
			{
				x.Property(x => x.X).IsRequired().HasColumnName("CamX");
				x.Property(x => x.Y).IsRequired().HasColumnName("CamY");
				x.Property(x => x.Z).IsRequired().HasColumnName("CamZ");
			});

			builder.ComplexProperty(x => x.PanCoordinates, x =>
			{
				x.Property(x => x.X).IsRequired().HasColumnName("PanX");
				x.Property(x => x.Y).IsRequired().HasColumnName("PanY");
				x.Property(x => x.Z).IsRequired().HasColumnName("PanZ");
			});

			return builder;
		}

		internal EntityTypeBuilder<Cad> SetValidaitons()
		{
			builder.Property(x => x.Key)
				.IsRequired()
				.HasColumnName(nameof(Cad.Key));

			builder.Property(x => x.ContentType)
				.IsRequired()
				.HasColumnName(nameof(Cad.ContentType));

			builder.Property(x => x.Volume)
				.IsRequired()
				.HasColumnName(nameof(Cad.Volume));

			builder.Property(x => x.OwnerId)
				.IsRequired()
				.HasColumnName(nameof(Cad.OwnerId));

			return builder;
		}
	}

}
