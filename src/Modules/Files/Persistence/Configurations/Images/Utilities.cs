using CustomCADs.Files.Domain.Images;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Files.Persistence.Configurations.Images;

using static DomainConstants.Textures;

public static class Utilities
{
	public static EntityTypeBuilder<Image> SetPrimaryKey(this EntityTypeBuilder<Image> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Image> SetStronglyTypedIds(this EntityTypeBuilder<Image> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				x => ImageId.New(x)
			);

		builder.Property(x => x.OwnerId)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				x => AccountId.New(x)
			);

		return builder;
	}

	public static EntityTypeBuilder<Image> SetValidaitons(this EntityTypeBuilder<Image> builder)
	{
		builder.Property(x => x.Key)
			.IsRequired()
			.HasColumnName(nameof(Image.Key));

		builder.Property(x => x.ContentType)
			.IsRequired()
			.HasColumnName(nameof(Image.ContentType));

		builder.Property(x => x.OwnerId)
			.IsRequired()
			.HasColumnName(nameof(Image.OwnerId));

		return builder;
	}

	public static EntityTypeBuilder<Image> SetSeeding(this EntityTypeBuilder<Image> builder)
	{
		AccountId adminId = AccountId.New(Guid.Parse(DomainConstants.Users.AdminAccountId));
		builder.HasData([
			Image.CreateWithId(PLA, "textures/pla.webp", "image/webp", adminId),
			Image.CreateWithId(ABS, "textures/abs.webp", "image/webp", adminId),
			Image.CreateWithId(GlowInDark, "textures/glow-in-dark.webp", "image/webp", adminId),
			Image.CreateWithId(TUF, "textures/tuf.webp", "image/webp", adminId),
			Image.CreateWithId(Wood, "textures/wood.webp", "image/webp", adminId),
		]);

		return builder;
	}
}
