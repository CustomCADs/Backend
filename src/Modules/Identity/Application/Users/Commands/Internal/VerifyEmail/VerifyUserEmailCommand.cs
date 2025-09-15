using CustomCADs.Identity.Application.Users.Dtos;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;

public sealed record VerifyUserEmailCommand(
	string Username,
	string Token
) : ICommand<TokensDto>;
