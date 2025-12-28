using CustomCADs.Modules.Catalog.Domain.Categories;

namespace CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

public interface ICategoryWrites
{
	Task<Category> AddAsync(Category entity, CancellationToken ct = default);
	void Remove(Category entity);
}
