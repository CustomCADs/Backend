namespace CustomCADs.Identity.API.Identity.Post.VerifyEmail;

public sealed record ConfirmEmailRequest(
	string Username,
	string Token
);
