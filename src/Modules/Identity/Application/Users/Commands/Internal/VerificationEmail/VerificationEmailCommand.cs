namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerificationEmail;

public sealed record VerificationEmailCommand(
	string Username
) : ICommand;
