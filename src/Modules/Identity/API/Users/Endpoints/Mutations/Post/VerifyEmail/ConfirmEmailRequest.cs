namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Post.VerifyEmail;

public sealed record ConfirmEmailRequest(
	string Username,
	string Token
);
