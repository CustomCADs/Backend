using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetShipmentSortingsQuery : IQuery<ShipmentSortingType[]>;
