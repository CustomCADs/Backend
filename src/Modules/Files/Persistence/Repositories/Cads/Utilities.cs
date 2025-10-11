using CustomCADs.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Persistence.Repositories.Cads;

public static class Utilities
{
	public static IQueryable<Cad> WithFilter(this IQueryable<Cad> query, CadId[]? ids, AccountId? ownerId)
	{
		if (ids is not null)
		{
			query = query.Where(x => ids.Contains(x.Id));
		}
		if (ownerId is not null)
		{
			query = query.Where(x => ownerId == x.OwnerId);
		}

		return query;
	}
}
