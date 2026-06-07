namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Post.Login;

public sealed record LoginRequest(
	string Username,
	string Password,
	bool? RememberMe = default
);
