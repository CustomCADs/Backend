using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete.Fingerprints;

public record DeleteFingerprintCommand(
	RefreshTokenId RefreshTokenId,
	string? CurrentRefreshToken,
	AccountId CallerId
) : ICommand;
