using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Customs.Persistence.Repositories.Customs;

internal static class Utilities
{
	extension(IQueryable<Custom> query)
	{
		internal IQueryable<Custom> WithFilter(bool? forDelivery = null, CustomStatus? status = null, AccountId? customerId = null, AccountId? designerId = null, CategoryId? categoryId = null)
		{
			if (forDelivery is not null)
			{
				query = query.Where(x => x.ForDelivery == forDelivery);
			}
			if (status is not null)
			{
				query = query.Where(x => x.CustomStatus == status);
			}
			if (customerId is not null)
			{
				query = query.Where(x => x.BuyerId == customerId);
			}
			if (designerId is not null)
			{
				query = query.Where(x => x.AcceptedCustom != null && x.AcceptedCustom.DesignerId == designerId);
			}
			if (categoryId is not null)
			{
				query = query.Where(x => x.Category != null && x.Category.Id == categoryId);
			}

			return query;
		}

		internal IQueryable<Custom> WithSearch(string? name = null)
		{
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
			}

			return query;
		}

		internal IQueryable<Custom> WithSorting(Sorting<CustomSortingType>? sorting = null)
			=> sorting?.Type switch
			{
				CustomSortingType.OrderedAt => query.ToSorted(sorting, x => x.OrderedAt),
				CustomSortingType.Alphabetical => query.ToSorted(sorting, x => x.Name),
				CustomSortingType.CustomStatus => query.ToSorted(sorting, x => (int)x.CustomStatus),
				_ => query,
			};
	}
}
