using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.SSO.Register;

public sealed record SingleSignOnUserCommand(
	string? Role,
	string? FirstName,
	string? LastName,
	string Username,
	string Email,
	string Provider,
	Fingerprint Fingerprint
) : ICommand<TokensDto>;
