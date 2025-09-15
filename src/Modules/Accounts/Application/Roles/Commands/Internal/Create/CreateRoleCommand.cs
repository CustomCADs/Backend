namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

public sealed record CreateRoleCommand(
	string Name,
	string Description
) : ICommand<RoleId>;
