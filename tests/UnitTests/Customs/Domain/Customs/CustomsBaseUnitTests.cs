using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Customs.Domain.Customs;

using static CustomsData;

public class CustomsBaseUnitTests
{
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

	protected static Custom CreateCustomWithStatus(CustomStatus status)
	{
		Custom custom = CreateCustom();

		switch (status)
		{
			case CustomStatus.Pending:
				break;

			case CustomStatus.Accepted:
				custom.Accept(ValidDesignerId);
				break;

			case CustomStatus.Begun:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				break;

			case CustomStatus.Finished:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				custom.Finish(ValidCadId, ValidPrice);
				break;

			case CustomStatus.Completed:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				custom.Finish(ValidCadId, ValidPrice);
				custom.Complete(customizationId: null);
				break;

			case CustomStatus.Reported:
				custom.Report();
				break;

			case CustomStatus.Removed:
				custom.Report();
				custom.Remove();
				break;
		}

		return custom;
	}
}
