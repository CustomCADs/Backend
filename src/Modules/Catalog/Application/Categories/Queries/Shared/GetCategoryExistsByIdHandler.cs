using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Categories.Queries.Shared;

public sealed class GetCategoryExistsByIdHandler(ICategoryReads reads)
	: IQueryHandler<GetCategoryExistsByIdQuery, bool>
{
	public async Task<bool> Handle(GetCategoryExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
}
