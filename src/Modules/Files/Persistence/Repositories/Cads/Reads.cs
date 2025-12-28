using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Files.Persistence.Repositories.Cads;

public sealed class Reads(FilesContext context) : ICadReads
{
	public async Task<Result<Cad>> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Cad> queryable = context.Cads
			.WithFilter(query.Ids, query.OwnerId);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Cad[] cads = await queryable
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, cads);
	}

	public async Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default)
		=> await context.Cads
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default)
		=> await context.Cads
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);
}
