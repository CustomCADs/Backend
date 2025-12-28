namespace CustomCADs.Modules.Identity.Application.Users.Dtos;

public record CookieSettings(string? Domain)
{
	public CookieSettings() : this(Domain: null) { }
}
