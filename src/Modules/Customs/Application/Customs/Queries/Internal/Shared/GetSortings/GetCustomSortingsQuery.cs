using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetCustomSortingsQuery : IQuery<CustomSortingType[]>;
