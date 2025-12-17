using CustomCADs.Modules.Identity.Application.Users.Dtos;
namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Refresh;

public sealed record RefreshUserCommand(
	string? Token
) : ICommand<TokensDto>;
