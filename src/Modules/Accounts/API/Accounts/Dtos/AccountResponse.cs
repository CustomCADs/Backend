namespace CustomCADs.Accounts.API.Accounts.Dtos;

public sealed record AccountResponse(
	string Username,
	string Email,
	string Role,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
