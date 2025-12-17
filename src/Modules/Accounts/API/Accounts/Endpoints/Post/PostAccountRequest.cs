namespace CustomCADs.Modules.Accounts.API.Accounts.Endpoints.Post;

public sealed record PostAccountRequest(
	string Role,
	string Username,
	string Email,
	string Password,
	string? FirstName = default,
	string? LastName = default
);
