namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;

public sealed record GetAllAccountsDto(
	AccountId Id,
	string Username,
	string Email,
	string Role,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
