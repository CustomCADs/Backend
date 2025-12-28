namespace CustomCADs.Modules.Catalog.API.Categories;

internal static class Mapper
{
	extension(CategoryReadDto category)
	{
		internal CategoryResponse ToResponse()
			=> new(category.Id.Value, category.Name, category.Description);
	}

}
