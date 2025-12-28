using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetByName;

public sealed class GetCategoryByNameHandler(
	ICategoryReads reads
) : IQueryHandler<GetCategoryByNameQuery, CategoryReadDto>
{
	public async Task<CategoryReadDto> Handle(GetCategoryByNameQuery req, CancellationToken ct)
	{
		Category category = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Category>.ByProp(nameof(Category.Name), req.Name);

		return category.ToDto();
	}
}
