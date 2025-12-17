namespace CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Create;

public sealed record CreateCategoryCommand(
	CategoryWriteDto Dto
) : ICommand<CategoryId>;
