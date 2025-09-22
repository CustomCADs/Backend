using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Customs.Application.Customs;

using static CustomsData;

public class CustomsBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	public static Custom CreateCustom(string? name = null, string? description = null, bool? forDelivery = null, AccountId? buyerId = null, CategoryId? categoryId = null, CustomCategorySetter? setter = null)
		=> Custom.Create(
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			forDelivery: forDelivery ?? false,
			buyerId: buyerId ?? ValidBuyerId,
			category: (categoryId ?? ValidCategoryId, setter ?? CustomCategorySetter.Customer)
		);

	public static Custom CreateCustomWithId(CustomId? id = null, string? name = null, string? description = null, bool? forDelivery = null, AccountId? buyerId = null, CategoryId? categoryId = null, CustomCategorySetter? setter = null)
		=> Custom.CreateWithId(
			id: id ?? ValidId,
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			delivery: forDelivery ?? false,
			buyerId: buyerId ?? ValidBuyerId,
			category: (categoryId ?? ValidCategoryId, setter ?? CustomCategorySetter.Customer)
		);
}
