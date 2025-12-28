using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs.Categories;

internal static class Utilities
{
	extension(EntityTypeBuilder<CustomCategory> builder)
	{
		internal EntityTypeBuilder<CustomCategory> SetPrimaryKey()
		{
			builder.HasKey(x => new { x.Id, x.CustomId });

			return builder;
		}

		internal EntityTypeBuilder<CustomCategory> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.HasConversion(
					x => x.Value,
					x => CategoryId.New(x)
				);

			builder.Property(x => x.CustomId)
				.HasConversion(
					x => x.Value,
					x => CustomId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<CustomCategory> SetNavigations()
		{
			builder
				.HasOne<Custom>()
				.WithOne(x => x.Category)
				.HasForeignKey<CustomCategory>(x => x.CustomId);

			return builder;
		}

		internal EntityTypeBuilder<CustomCategory> SetValidations()
		{
			builder.Property(x => x.Id)
				.IsRequired()
				.HasColumnName(nameof(CustomCategory.Id));

			builder.Property(x => x.SetAt)
				.IsRequired()
				.HasColumnName(nameof(CustomCategory.SetAt));

			builder.Property(x => x.Setter)
				.IsRequired()
				.HasColumnName(nameof(CustomCategory.Setter))
				.HasConversion(
					x => x.ToString(),
					x => Enum.Parse<CustomCategorySetter>(x)
				);

			return builder;
		}
	}

}
