using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.SetCategory;

public sealed record AdminSetCustomCategoryCommand(
	CustomId Id,
	CategoryId CategoryId
) : ICommand;
