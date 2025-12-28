using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

public interface ITagWrites
{
	Task<Tag> AddAsync(Tag tag, CancellationToken ct = default);
	void Remove(Tag tag);
}
