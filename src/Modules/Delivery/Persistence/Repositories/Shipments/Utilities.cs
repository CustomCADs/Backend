using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Delivery.Persistence.Repositories.Shipments;

public static class Utilities
{
	public static IQueryable<Shipment> WithFilter(this IQueryable<Shipment> query, AccountId? clientId = null)
	{
		if (clientId is not null)
		{
			query = query.Where(x => x.BuyerId == clientId);
		}

		return query;
	}

	public static IQueryable<Shipment> WithSorting(this IQueryable<Shipment> query, Sorting<ShipmentSortingType>? sorting = null)
		=> sorting?.Type switch
		{
			ShipmentSortingType.RequestedAt => query.ToSorted(sorting, x => x.RequestedAt),
			ShipmentSortingType.Country => query.ToSorted(sorting, x => x.Address.Country),
			ShipmentSortingType.City => query.ToSorted(sorting, x => x.Address.City),
			_ => query,
		};
}
