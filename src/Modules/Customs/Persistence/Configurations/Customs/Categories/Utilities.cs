using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Categories;

public static class Utilities
{
	public static EntityTypeBuilder<CustomCategory> SetPrimaryKey(this EntityTypeBuilder<CustomCategory> builder)
	{
		builder.HasKey(x => new { x.Id, x.CustomId });

		return builder;
	}

	public static EntityTypeBuilder<CustomCategory> SetStronglyTypedIds(this EntityTypeBuilder<CustomCategory> builder)
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

	public static EntityTypeBuilder<CustomCategory> SetNavigations(this EntityTypeBuilder<CustomCategory> builder)
	{
		builder
			.HasOne<Custom>()
			.WithOne(x => x.Category)
			.HasForeignKey<CustomCategory>(x => x.CustomId);

		return builder;
	}

	public static EntityTypeBuilder<CustomCategory> SetValidations(this EntityTypeBuilder<CustomCategory> builder)
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
