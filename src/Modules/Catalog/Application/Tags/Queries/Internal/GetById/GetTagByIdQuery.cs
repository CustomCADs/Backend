using CustomCADs.Modules.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetById;

public sealed record GetTagByIdQuery(
	TagId Id
) : IQuery<TagDto>;
