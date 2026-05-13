using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;

public sealed record DeleteUserCommand(AccountId CallerId) : ICommand;
