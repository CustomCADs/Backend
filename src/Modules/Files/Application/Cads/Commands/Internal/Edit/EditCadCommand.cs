using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Cads.Commands.Internal.Edit;

public sealed record EditCadCommand(
	CadId Id,
	string ContentType,
	decimal Volume,
	AccountId CallerId
) : ICommand;
