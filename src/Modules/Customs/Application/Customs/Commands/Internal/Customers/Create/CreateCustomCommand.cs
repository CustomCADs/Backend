using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Create;

public sealed record CreateCustomCommand(
	string Name,
	string Description,
	bool ForDelivery,
	AccountId CallerId,
	CategoryId? CategoryId
) : ICommand<CustomId>;
