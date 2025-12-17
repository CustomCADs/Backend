namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;

public sealed record DeleteUserCommand(
	string Username
) : ICommand;
