using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Shared;

public sealed class CadExistsByIdHandler(ICadReads reads)
	: IQueryHandler<CadExistsByIdQuery, bool>
{
	public async Task<bool> Handle(CadExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);
}
