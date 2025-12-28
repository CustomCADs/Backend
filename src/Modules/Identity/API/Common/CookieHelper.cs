using CustomCADs.Modules.Identity.Application.Users.Dtos;

namespace CustomCADs.Modules.Identity.API.Common;

public static class CookieHelper
{
	private const string AccessTokenCookie = "jwt";
	private const string RefreshTokenCookie = "rt";
	private const string CsrfTokenCookie = "csrf";
	private const string RoleCookie = "role";
	private const string UsernameCookie = "username";

	extension(HttpContext context)
	{
		public string? RefreshTokenCookie => context.Request.Cookies.FirstOrDefault(x => x.Key == RefreshTokenCookie).Value;

		private void SaveAccessToken(TokenDto jwt, string? domain)
			=> context.Response.Cookies.Append(AccessTokenCookie, jwt.Value, CookieOptions(jwt.ExpiresAt, httpOnly: true, domain: domain));

		private void SaveRefreshToken(TokenDto rt, string? domain)
			=> context.Response.Cookies.Append(RefreshTokenCookie, rt.Value, CookieOptions(rt.ExpiresAt, httpOnly: true, domain: domain));

		private void SaveCsrfToken(TokenDto csrf, string? domain)
			=> context.Response.Cookies.Append(CsrfTokenCookie, csrf.Value, CookieOptions(csrf.ExpiresAt, domain: domain));

		private void SaveUsername(string username, DateTimeOffset expire, string? domain)
			=> context.Response.Cookies.Append(UsernameCookie, username, CookieOptions(expire, domain: domain));

		private void SaveRole(string role, DateTimeOffset expire, string? domain)
			=> context.Response.Cookies.Append(RoleCookie, role, CookieOptions(expire, domain: domain));

		public void RefreshCookies(
			TokenDto access,
			TokenDto csrf,
			string? domain
		)
		{
			context.SaveAccessToken(access, domain);
			context.SaveCsrfToken(csrf, domain);
		}

		public void SaveAllCookies(
			TokensDto tokens,
			string username,
			string? domain
		)
		{
			context.SaveAccessToken(tokens.AccessToken, domain);
			context.SaveCsrfToken(tokens.CsrfToken, domain);
			context.SaveRefreshToken(tokens.RefreshToken, domain);
			context.SaveRole(tokens.Role, tokens.RefreshToken.ExpiresAt, domain);
			context.SaveUsername(username, tokens.RefreshToken.ExpiresAt, domain);
		}

		public void DeleteAllCookies(string? domain)
		{
			DeleteCookie(AccessTokenCookie, httpOnly: true, domain: domain);
			DeleteCookie(RefreshTokenCookie, httpOnly: true, domain: domain);
			DeleteCookie(CsrfTokenCookie, domain: domain);
			DeleteCookie(RoleCookie, domain: domain);
			DeleteCookie(UsernameCookie, domain: domain);

			void DeleteCookie(string key, bool httpOnly = false, string? domain = null)
			{
				context.Response.Cookies.Append(
					key: key,
					value: string.Empty,
					options: CookieOptions(DateTimeOffset.UnixEpoch, httpOnly, domain)
				);
			}
		}
	}

	private static CookieOptions CookieOptions(DateTimeOffset expire, bool httpOnly = false, string? domain = null)
		=> new()
		{
			HttpOnly = httpOnly,
			Domain = domain,
			Secure = true,
			Expires = expire,
			SameSite = SameSiteMode.None,
		};
}
