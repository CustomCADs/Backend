namespace CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Delete;

public sealed record DeleteCategoryCommand(
	CategoryId Id
) : ICommand;
