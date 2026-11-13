using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetByName;

public sealed class GetTagByNameHandler(ITagReads reads)
	: IQueryHandler<GetTagByNameQuery, TagDto>
{
	public async Task<TagDto> Handle(GetTagByNameQuery req, CancellationToken ct)
	{
		Tag tag = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Tag>.ByProp(nameof(req.Name), req.Name);

		return tag.ToDto();
	}
}
