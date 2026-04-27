namespace CustomCADs.Modules.Identity.API.Identity.Patch.Names;

public sealed record ChangeNamesRequest(
	string Username,
	string? FirstName,
	string? LastName
);
