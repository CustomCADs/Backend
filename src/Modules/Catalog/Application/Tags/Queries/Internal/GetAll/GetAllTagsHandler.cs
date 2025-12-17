using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetAll;

public sealed class GetAllTagsHandler(ITagReads reads, BaseCachingService<TagId, Tag> cache)
	: IQueryHandler<GetAllTagsQuery, TagDto[]>
{
	public async Task<TagDto[]> Handle(GetAllTagsQuery req, CancellationToken ct)
	{
		Tag[] tags = [.. await cache.GetOrCreateAsync(
			factory: async () => await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)
		).ConfigureAwait(false)];

		return [.. tags.Select(x => x.ToDto())];
	}
}
