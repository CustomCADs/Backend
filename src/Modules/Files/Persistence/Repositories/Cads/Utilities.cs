using CustomCADs.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Persistence.Repositories.Cads;

public static class Utilities
{
	public static IQueryable<Cad> WithFilter(this IQueryable<Cad> query, CadId[]? ids)
	{
		if (ids is not null)
		{
			query = query.Where(x => ids.Contains(x.Id));
		}

		return query;
	}
}
