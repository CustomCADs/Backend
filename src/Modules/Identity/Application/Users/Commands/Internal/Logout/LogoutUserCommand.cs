namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Logout;

public sealed record LogoutUserCommand(
	string? RefreshToken
) : ICommand;
