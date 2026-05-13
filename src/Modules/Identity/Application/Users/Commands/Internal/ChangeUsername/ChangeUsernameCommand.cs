using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

public sealed record ChangeUsernameCommand(
	AccountId Id,
	string Username,
	string? FirstName,
	string? LastName
) : ICommand;
