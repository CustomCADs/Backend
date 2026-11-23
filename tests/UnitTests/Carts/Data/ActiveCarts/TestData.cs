using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Data.ActiveCarts;

using static ActiveCartItemConstants;

public static class TestData
{
	public const int MinValidQuantity = QuantityMin + 1;
	public const int MaxValidQuantity = QuantityMax - 1;
	public const int MaxInvalidQuantity = QuantityMax + 1;
	public const int MinInvalidQuantity = QuantityMin - 1;

	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly ProductId ValidProductId = ProductId.New();
	public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
}
