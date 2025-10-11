using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Cads.Commands.Internal.Create;

public sealed record CreateCadCommand(
	string Key,
	string ContentType,
	decimal Volume,
	AccountId CallerId
) : ICommand<CadId>;
