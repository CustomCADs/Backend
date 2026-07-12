using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Customs;

namespace CustomCADs.UnitTests.Customs.Data.Customs;

using static TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static readonly Func<Action, InvalidOperationException> ExpectValidationException
		= Assert.Throws<InvalidOperationException>;

	public static Custom CreateCustom(
		string? name = null,
		string? description = null,
		bool? forDelivery = null,
		AccountId? buyerId = null,
		CategoryId? categoryId = null,
		CustomCategorySetter? setter = null,
		CustomId? id = null
	)
		=> Custom.CreateWithId(
			id: id ?? ValidId,
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			forDelivery: forDelivery ?? false,
			buyerId: buyerId ?? ValidBuyerId,
			category: (categoryId ?? ValidCategoryId, setter ?? CustomCategorySetter.Customer)
		);
}
