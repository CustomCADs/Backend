using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Edit;

public sealed record EditCustomCommand(
	CustomId Id,
	string Name,
	string Description,
	CategoryId? CategoryId,
	AccountId CallerId
) : ICommand;
