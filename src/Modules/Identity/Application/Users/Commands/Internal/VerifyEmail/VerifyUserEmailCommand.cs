using CustomCADs.Modules.Identity.Application.Users.Dtos;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerifyEmail;

public sealed record VerifyUserEmailCommand(
	string Username,
	string Token
) : ICommand<TokensDto>;
