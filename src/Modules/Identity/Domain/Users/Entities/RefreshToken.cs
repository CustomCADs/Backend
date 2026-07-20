using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Modules.Identity.Domain.Users.Entities;

using ValueObjects;
using static Constants.Tokens;

public class RefreshToken : BaseEntity
{
	private RefreshToken() { }
	private RefreshToken(string value, Fingerprint fingerprint, UserId userId, bool longerSession) : this()
	{
		Value = value;
		Fingerprint = fingerprint;
		UserId = userId;
		IssuedAt = DateTimeOffset.UtcNow;
		ExpiresAt = IssuedAt.AddDays(
			longerSession ? LongerRtDurationInDays : RtDurationInDays
		);
	}
	private RefreshToken(string value, Fingerprint fingerprint, UserId userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
	{
		Value = value;
		Fingerprint = fingerprint;
		UserId = userId;
		IssuedAt = issuedAt;
		ExpiresAt = expiresAt;
	}

	public RefreshTokenId Id { get; init; }
	public DateTimeOffset IssuedAt { get; init; }
	public DateTimeOffset ExpiresAt { get; init; }
	public string Value { get; private set; } = string.Empty;
	public Fingerprint Fingerprint { get; private set; } = new();
	public UserId UserId { get; private set; }

	public static RefreshToken Create(string value, Fingerprint fingerprint, UserId userId, bool longerSession)
		=> new(value, fingerprint, userId, longerSession);

	public static RefreshToken Create(RefreshTokenId id, string value, Fingerprint fingerprint, UserId userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
		=> new(value, fingerprint, userId, issuedAt, expiresAt)
		{
			Id = id,
		};
}
