namespace CustomCADs.Modules.Identity.API.Dtos;

public record FingerprintResponse(
	Guid Id,
	string Device,
	string? Location,
	bool DeleteAllowed,
	DateTimeOffset IssuedAt
);
