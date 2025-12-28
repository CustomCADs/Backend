using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Files.Persistence.Repositories.Cads;

internal static class Utilities
{
	extension(IQueryable<Cad> query)
	{
		internal IQueryable<Cad> WithFilter(CadId[]? ids, AccountId? ownerId)
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

}
