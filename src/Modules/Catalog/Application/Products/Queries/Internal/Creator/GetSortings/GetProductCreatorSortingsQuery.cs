using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetProductCreatorSortingsQuery : IQuery<ProductCreatorSortingType[]>;
