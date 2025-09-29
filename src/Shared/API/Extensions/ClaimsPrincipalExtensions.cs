using CustomCADs.Shared.Domain.TypedIds.Accounts;
using System.Security.Claims;

namespace CustomCADs.Shared.API.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static AccountId GetAccountId(this ClaimsPrincipal user)
		=> AccountId.New(user.FindFirstValue(ClaimTypes.NameIdentifier));

	public static string GetName(this ClaimsPrincipal user)
		=> user.Identity?.Name ?? string.Empty;

	public static bool GetAuthentication(this ClaimsPrincipal user)
		=> user.Identity?.IsAuthenticated ?? false;

	public static string? GetAuthorization(this ClaimsPrincipal user)
		=> user.FindFirstValue(ClaimTypes.Role);

	public static void ExtractUserFromSSO(this ClaimsPrincipal? user, out string email, out string username)
	{
		if (user is null)
		{
			throw new Exception("Claims required");
		}

		email = user.Claims?.FirstOrDefault(c => c.Type switch
		{
			ClaimTypes.Email => true,
			"email" => true,
			"r_emailaddress" => true,
			_ => false
		})?.Value ?? throw new Exception("Email required");

		username = user.Claims?.FirstOrDefault(c => c.Type switch
		{
			ClaimTypes.Name => true,
			"preferred_username" => true,
			"name" => true,
			_ => false,
		})?.Value ?? email.Split('@').First();
	}
}

