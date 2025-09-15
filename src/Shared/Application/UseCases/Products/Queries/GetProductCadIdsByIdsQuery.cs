using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Shared.Application.UseCases.Products.Queries;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetProductCadIdsByIdsQuery(
	ProductId[] Ids
) : IQuery<Dictionary<ProductId, CadId>>;
