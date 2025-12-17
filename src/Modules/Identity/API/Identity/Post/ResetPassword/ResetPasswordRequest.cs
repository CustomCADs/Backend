namespace CustomCADs.Modules.Identity.API.Identity.Post.ResetPassword;

public sealed record ResetPasswordRequest(string Email, string Token, string NewPassword);
