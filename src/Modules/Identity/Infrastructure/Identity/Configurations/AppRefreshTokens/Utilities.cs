using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppRefreshTokens;

internal static class Utilities
{
	extension(EntityTypeBuilder<AppRefreshToken> builder)
	{
		internal EntityTypeBuilder<AppRefreshToken> SetValidations()
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

}
