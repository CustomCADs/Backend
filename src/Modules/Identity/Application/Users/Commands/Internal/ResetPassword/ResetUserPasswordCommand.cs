namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPassword;

public sealed record ResetUserPasswordCommand(
	string Email,
	string Token,
	string NewPassword
) : ICommand;
