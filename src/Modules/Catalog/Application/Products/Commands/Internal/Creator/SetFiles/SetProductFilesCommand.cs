using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;

public sealed record SetProductFilesCommand(
	ProductId Id,
	CadDto? Cad,
	ImageDto? Image,
	AccountId CallerId
) : ICommand;
