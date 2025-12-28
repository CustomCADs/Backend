using CustomCADs.Modules.Identity.Application.Users.Dtos;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;

public sealed record LoginUserCommand(
	string Username,
	string Password,
	bool LongerExpireTime
) : ICommand<TokensDto>;
