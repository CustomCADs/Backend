namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

public sealed record ChangeUsernameCommand(
	string Username,
	string NewUsername
) : ICommand;
