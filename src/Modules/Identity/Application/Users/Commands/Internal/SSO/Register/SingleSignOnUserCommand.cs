using CustomCADs.Modules.Identity.Application.Users.Dtos;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.SSO.Register;

public sealed record SingleSignOnUserCommand(
	string? Role,
	string Username,
	string Email,
	string Provider
) : ICommand<TokensDto>;
