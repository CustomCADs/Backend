using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCad;

public sealed record SetProductCadCommand(
	ProductId Id,
	string ContentType,
	decimal Volume,
	AccountId CallerId
) : ICommand;
