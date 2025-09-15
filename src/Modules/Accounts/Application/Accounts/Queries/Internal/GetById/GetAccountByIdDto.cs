namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;

public sealed record GetAccountByIdDto(
	AccountId Id,
	string Role,
	string Username,
	string Email,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
