using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;

public sealed record DesignerSetCustomCategoryCommand(
	CustomId Id,
	CategoryId CategoryId,
	AccountId CallerId
) : ICommand;
