using CustomCADs.Shared.Domain.TypedIds.Accounts;
using System.Security.Claims;

namespace CustomCADs.Shared.API.Extensions;

public static class ClaimsPrincipalExtensions
{
	extension(ClaimsPrincipal user)
	{
		public AccountId AccountId => AccountId.New(user.FindFirstValue(ClaimTypes.NameIdentifier));
		public string Name => user.Identity?.Name ?? string.Empty;
		public bool IsAuthenticated => user.Identity?.IsAuthenticated ?? false;
		public string? Authorization => user.FindFirstValue(ClaimTypes.Role);

		public void ExtractUserFromSSO(out string email, out string username)
		{
			email = user.Claims.FirstOrDefault(c => c.Type switch
			{
				ClaimTypes.Email => true,
				"email" => true,
				"r_emailaddress" => true,
				_ => false
			})?.Value ?? throw new Exception("Email required");

			username = user.Claims.FirstOrDefault(c => c.Type switch
			{
				ClaimTypes.Name => true,
				"preferred_username" => true,
				"name" => true,
				_ => false,
			})?.Value ?? email.Split('@').First();
		}
	}
}

