using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetProductCreatorSortingsQuery : IQuery<ProductCreatorSortingType[]>;
