using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;

public sealed record LoginUserCommand(
	string Username,
	string Password,
	bool LongerExpireTime,
	Fingerprint Fingerprint
) : ICommand<TokensDto>;
