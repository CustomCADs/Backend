namespace CustomCADs.Modules.Identity.API.Identity.Post.Login;

public sealed record LoginRequest(
	string Username,
	string Password,
	bool? RememberMe = default
);
