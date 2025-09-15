using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Infrastructure.Identity.Configurations.AppRefreshTokens;

public static class Utilities
{
	public static EntityTypeBuilder<AppRefreshToken> SetValidations(this EntityTypeBuilder<AppRefreshToken> builder)
	{
		builder.Property(x => x.Value)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.Value));

		builder.Property(x => x.IssuedAt)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.IssuedAt));

		builder.Property(x => x.ExpiresAt)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.ExpiresAt));

		builder.Property(x => x.UserId)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.UserId));

		return builder;
	}
}
