using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs;

using static CustomConstants;

internal static class Utilities
{
	extension(EntityTypeBuilder<Custom> builder)
	{
		internal EntityTypeBuilder<Custom> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Custom> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => CustomId.New(x)
				);

			builder.Property(x => x.BuyerId)
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Custom> SetValidations()
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Custom.Name));

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(DescriptionMaxLength)
				.HasColumnName(nameof(Custom.Description));

			builder.Property(x => x.ForDelivery)
				.IsRequired()
				.HasColumnName(nameof(Custom.ForDelivery));

			builder.Property(x => x.OrderedAt)
				.IsRequired()
				.HasColumnName(nameof(Custom.OrderedAt));

			builder.Property(x => x.CustomStatus)
				.IsRequired()
				.HasColumnName(nameof(Custom.CustomStatus))
				.HasConversion(
					x => x.ToString(),
					s => Enum.Parse<CustomStatus>(s)
				);

			builder.Property(x => x.BuyerId)
				.IsRequired()
				.HasColumnName(nameof(Custom.BuyerId));

			return builder;
		}
	}

}
