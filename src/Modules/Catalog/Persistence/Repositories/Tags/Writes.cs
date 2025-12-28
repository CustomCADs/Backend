using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Persistence.Repositories.Tags;

public class Writes(CatalogContext context) : ITagWrites
{
	public async Task<Tag> AddAsync(Tag tag, CancellationToken ct = default)
		=> (await context.Tags.AddAsync(tag, ct).ConfigureAwait(false)).Entity;

	public void Remove(Tag tag)
		=> context.Tags.Remove(tag);
}
