namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Patch.Names;

public sealed record ChangeNamesRequest(
	string Username,
	string? FirstName,
	string? LastName
);
