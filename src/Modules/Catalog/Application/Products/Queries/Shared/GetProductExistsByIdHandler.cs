using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared;

public sealed class GetProductExistsByIdHandler(IProductReads reads)
	: IQueryHandler<GetProductExistsByIdQuery, bool>
{
	public async Task<bool> Handle(GetProductExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
}
