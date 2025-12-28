using CustomCADs.Modules.Catalog.Domain.Tags;
using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Catalog.Persistence.Configurations.Tags;

using static DomainConstants.Tags;
using static TagConstants;

internal static class Utilities
{
	extension(EntityTypeBuilder<Tag> builder)
	{
		internal EntityTypeBuilder<Tag> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Tag> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => TagId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Tag> SetValidations()
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Tag.Name));

			return builder;
		}

		internal EntityTypeBuilder<Tag> SetSeeding()
		{
			builder.HasData([
				Tag.CreateWithId(NewId, New),
				Tag.CreateWithId(ProfessionalId, Professional),
				Tag.CreateWithId(PrintableId, Printable),
				Tag.CreateWithId(PopularId, Popular),
			]);

			return builder;
		}
	}

}
