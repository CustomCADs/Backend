namespace CustomCADs.Modules.Identity.Infrastructure.Tokens;

public record JwtSettings(
	string SecretKey,
	string Issuer,
	string Audience
)
{
	public JwtSettings() : this("", "", "") { }
}
