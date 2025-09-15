using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;

public sealed class GetShipmentSortingsHandler : IQueryHandler<GetShipmentSortingsQuery, ShipmentSortingType[]>
{
	public Task<ShipmentSortingType[]> Handle(GetShipmentSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<ShipmentSortingType>()
		);
}
