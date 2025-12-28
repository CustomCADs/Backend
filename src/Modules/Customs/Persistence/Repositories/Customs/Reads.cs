using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Customs.Persistence.Repositories.Customs;

public sealed class Reads(CustomsContext context) : ICustomReads
{
	public async Task<Result<Custom>> AllAsync(CustomQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Custom> queryable = context.Customs
			.WithTracking(track)
			.Include(x => x.Category)
			.Include(x => x.AcceptedCustom)
			.WithFilter(query.ForDelivery, query.CustomStatus, query.CustomerId, query.DesignerId, query.CategoryId)
			.WithSearch(query.Name);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Custom[] customs = await queryable
			.WithSorting(query.Sorting)
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, customs);
	}

	public async Task<Custom?> SingleByIdAsync(CustomId id, bool track = true, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(track)
			.Include(x => x.Category)
			.Include(x => x.AcceptedCustom)
			.Include(x => x.FinishedCustom)
			.Include(x => x.CompletedCustom)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Custom?> SingleByCadIdAsync(CadId cadId, bool track = true, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(track)
			.Include(x => x.Category)
			.Include(x => x.AcceptedCustom)
			.Include(x => x.FinishedCustom)
			.Include(x => x.CompletedCustom)
			.FirstOrDefaultAsync(x => x.FinishedCustom != null && x.FinishedCustom.CadId == cadId, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(CustomId id, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByCadIdAsync(CadId cadId, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(false)
			.AnyAsync(x => x.FinishedCustom != null && x.FinishedCustom.CadId == cadId, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<CustomStatus, int>> CountAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.Customs
			.WithTracking(false)
			.Where(x => x.BuyerId == buyerId)
			.GroupBy(x => x.CustomStatus)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
