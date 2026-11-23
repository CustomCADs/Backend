using CustomCADs.Modules.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetByName;

public sealed record GetTagByNameQuery(
	string Name
) : IQuery<TagDto>;
