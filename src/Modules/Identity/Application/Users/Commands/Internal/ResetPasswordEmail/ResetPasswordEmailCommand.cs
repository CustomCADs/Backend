namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public sealed record ResetPasswordEmailCommand(
	string Email
) : ICommand;
