using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;
namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Refresh;

public sealed record RefreshUserCommand(
	string? Token,
	Fingerprint Fingerprint
) : ICommand<TokensDto>;
