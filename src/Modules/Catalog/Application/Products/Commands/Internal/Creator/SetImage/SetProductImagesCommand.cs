using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetImage;

public sealed record SetProductImageCommand(
	ProductId Id,
	string ContentType,
	AccountId CallerId
) : ICommand;
