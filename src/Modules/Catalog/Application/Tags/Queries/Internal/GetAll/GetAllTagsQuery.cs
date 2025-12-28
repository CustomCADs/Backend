using CustomCADs.Modules.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetAll;

public sealed record GetAllTagsQuery
 : IQuery<TagDto[]>;
