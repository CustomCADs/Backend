using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerifyEmail;

public sealed record VerifyUserEmailCommand(
	string Username,
	string Token,
	Fingerprint Fingerprint
) : ICommand<TokensDto>;
