using CustomCADs.Modules.Catalog.Domain.Products;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Catalog.Persistence.Configurations.Products;

using static ProductConstants;

internal static class Utilities
{
	extension(EntityTypeBuilder<Product> builder)
	{
		internal EntityTypeBuilder<Product> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Product> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => ProductId.New(x)
				);

			builder.Property(x => x.CategoryId)
				.HasConversion(
					x => x.Value,
					x => CategoryId.New(x)
				);

			builder.Property(x => x.ImageId)
				.HasConversion(
					x => x.Value,
					x => ImageId.New(x)
				);

			builder.Property(x => x.CadId)
				.HasConversion(
					x => x.Value,
					x => CadId.New(x)
				);

			builder.Property(x => x.CreatorId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			builder.Property(x => x.DesignerId)
				.HasConversion(
					x => AccountId.Unwrap(x),
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Product> SetValueObjects()
		{
			builder.OwnsOne(x => x.Counts, x =>
			{
				x.Property(x => x.Purchases)
					.IsRequired()
					.HasColumnName(nameof(Product.Counts.Purchases));

				x.Property(x => x.Views)
					.IsRequired()
					.HasColumnName(nameof(Product.Counts.Views));
			});

			return builder;
		}

		internal EntityTypeBuilder<Product> SetValidations()
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Product.Name));

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(DescriptionMaxLength)
				.HasColumnName(nameof(Product.Description));

			builder.Property(x => x.Price)
				.IsRequired()
				.HasPrecision(19, 2)
				.HasColumnName(nameof(Product.Price));

			builder.Property(x => x.Status)
				.IsRequired()
				.HasColumnName(nameof(Product.Status))
				.HasConversion(
					e => e.ToString(),
					s => Enum.Parse<ProductStatus>(s)
				);

			builder.Property(x => x.UploadedAt)
				.IsRequired()
				.HasColumnName(nameof(Product.UploadedAt));

			builder.Property(x => x.CategoryId)
				.IsRequired()
				.HasColumnName(nameof(Product.CategoryId));

			builder.Property(x => x.ImageId)
				.IsRequired()
				.HasColumnName(nameof(Product.ImageId));

			builder.Property(x => x.CadId)
				.IsRequired()
				.HasColumnName(nameof(Product.CadId));

			builder.Property(x => x.CreatorId)
				.IsRequired()
				.HasColumnName(nameof(Product.CreatorId));

			builder.Property(x => x.DesignerId)
				.HasColumnName(nameof(Product.DesignerId));

			return builder;
		}
	}

}
