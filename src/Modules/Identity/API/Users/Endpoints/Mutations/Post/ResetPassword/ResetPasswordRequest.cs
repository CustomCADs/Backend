namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Post.ResetPassword;

public sealed record ResetPasswordRequest(string Email, string Token, string NewPassword);
