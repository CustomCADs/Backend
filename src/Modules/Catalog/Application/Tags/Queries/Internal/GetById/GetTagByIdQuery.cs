using CustomCADs.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

public sealed record GetTagByIdQuery(
	TagId Id
) : IQuery<TagDto>;
