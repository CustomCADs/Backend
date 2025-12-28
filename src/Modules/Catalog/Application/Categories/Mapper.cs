namespace CustomCADs.Modules.Catalog.Application.Categories;

internal static class Mapper
{
	extension(Category category)
	{
		internal CategoryReadDto ToDto()
			=> new(category.Id, category.Name, category.Description);
	}

}
