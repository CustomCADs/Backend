using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;

public sealed record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	CategoryId CategoryId,
	ImageId ImageId,
	CadId CadId,
	AccountId CallerId
) : ICommand<ProductId>;
