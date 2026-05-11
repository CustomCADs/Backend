namespace CustomCADs.Modules.Identity.Application.Users.Dtos;

public record FingerprintDto(
	RefreshTokenId Id,
	string Device,
	string? Location,
	bool DeleteAllowed,
	DateTimeOffset IssuedAt
);
