using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;

public class AppRefreshToken
{
	public AppRefreshToken() { }
	public AppRefreshToken(string value, Fingerprint fingerprint, Guid userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
	{
		Value = value;
		Fingerprint = fingerprint;
		UserId = userId;
		IssuedAt = issuedAt;
		ExpiresAt = expiresAt;
	}

	public Guid Id { get; init; }
	public DateTimeOffset IssuedAt { get; init; }
	public DateTimeOffset ExpiresAt { get; init; }
	public string Value { get; private set; } = string.Empty;
	public Fingerprint Fingerprint { get; private set; } = new();
	public Guid UserId { get; private set; }
	public AppUser User { get; init; } = null!;
}
