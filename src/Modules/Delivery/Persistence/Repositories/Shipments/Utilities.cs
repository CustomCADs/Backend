using CustomCADs.Modules.Delivery.Domain.Shipments;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Delivery.Persistence.Repositories.Shipments;

internal static class Utilities
{
	extension(IQueryable<Shipment> query)
	{
		internal IQueryable<Shipment> WithFilter(AccountId? clientId = null)
		{
			if (clientId is not null)
			{
				query = query.Where(x => x.BuyerId == clientId);
			}

			return query;
		}

		internal IQueryable<Shipment> WithSorting(Sorting<ShipmentSortingType>? sorting = null)
			=> sorting?.Type switch
			{
				ShipmentSortingType.RequestedAt => query.ToSorted(sorting, x => x.RequestedAt),
				ShipmentSortingType.Country => query.ToSorted(sorting, x => x.Address.Country),
				ShipmentSortingType.City => query.ToSorted(sorting, x => x.Address.City),
				_ => query,
			};
	}

}
