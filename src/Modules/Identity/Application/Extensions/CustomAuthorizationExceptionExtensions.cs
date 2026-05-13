using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.Modules.Identity.Application.Extensions;

public static class CustomAuthorizationExceptionExtensions
{
	extension(CustomAuthorizationException<User> ex)
	{
		public static CustomAuthorizationException<User> NoRefreshToken()
			=> CustomAuthorizationException<User>.Custom("No Refresh Token found.");

		public static CustomAuthorizationException<User> RefreshTokenExpired()
			=> CustomAuthorizationException<User>.Custom("Refresh Token found, but expired.");
	}
}
