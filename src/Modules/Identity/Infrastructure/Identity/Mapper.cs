using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Domain.TypedIds.Identity;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity;

internal static class Mapper
{
	private static class Shallow
	{
		internal static RefreshToken ToRefreshToken(AppRefreshToken rt)
			=> RefreshToken.Create(
				id: RefreshTokenId.New(rt.Id),
				value: rt.Value,
				userId: UserId.New(rt.UserId),
				issuedAt: rt.IssuedAt,
				expiresAt: rt.ExpiresAt
			);

		internal static AppRefreshToken ToAppRefreshToken(RefreshToken rt)
			=> new(rt.Value, rt.UserId.Value, rt.IssuedAt, rt.ExpiresAt)
			{
				Id = rt.Id.Value,
			};
	}

	extension(AppUser appUser)
	{
		internal User ToUser(string role)
			=> User.Create(
				id: UserId.New(appUser.Id),
				role: role,
				username: appUser.Username,
				email: new(appUser.Email ?? string.Empty, appUser.EmailConfirmed),
				accountId: appUser.AccountId,
				refreshTokens: [.. appUser.RefreshTokens.Select(x => Shallow.ToRefreshToken(x))]
			);
	}

	extension(User user)
	{
		internal AppUser ToAppUser()
			=> new AppUser()
			{
				Id = user.Id.Value,
				IsSSO = false,
				Provider = null,
				UserName = user.Username,
				Email = user.Email.Value,
				EmailConfirmed = user.Email.IsVerified,
				AccountId = user.AccountId,
			}.FillRefreshTokens([.. user.RefreshTokens.Select(x => Shallow.ToAppRefreshToken(x))]);


		internal AppUser ToAppUser(string provider)
			=> new AppUser()
			{
				Id = user.Id.Value,
				IsSSO = true,
				Provider = provider,
				Username = user.Username,
				Email = user.Email.Value,
				EmailConfirmed = true,
				AccountId = user.AccountId,
			}.FillRefreshTokens([.. user.RefreshTokens.Select(x => Shallow.ToAppRefreshToken(x))]);
	}

	extension(AppRefreshToken rt)
	{
		internal RefreshToken ToRefreshToken()
			=> RefreshToken.Create(
				id: RefreshTokenId.New(rt.Id),
				value: rt.Value,
				userId: UserId.New(rt.UserId),
				issuedAt: rt.IssuedAt,
				expiresAt: rt.ExpiresAt
			);
	}

	extension(RefreshToken rt)
	{
		internal AppRefreshToken ToAppRefreshToken()
			=> new(rt.Value, rt.UserId.Value, rt.IssuedAt, rt.ExpiresAt)
			{
				Id = rt.Id.Value,
			};
	}

}
