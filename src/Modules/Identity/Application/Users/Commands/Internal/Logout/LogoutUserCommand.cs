namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public sealed record LogoutUserCommand(
	string? RefreshToken
) : ICommand;
