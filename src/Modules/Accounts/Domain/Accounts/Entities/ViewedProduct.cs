using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Accounts.Domain.Accounts.Entities;

public class ViewedProduct
{
	private ViewedProduct() { }
	private ViewedProduct(AccountId id, ProductId productId, DateTimeOffset viewedAt) : this()
	{
		AccountId = id;
		ProductId = productId;
		ViewedAt = viewedAt;
	}

	public AccountId AccountId { get; init; }
	public ProductId ProductId { get; init; }
	public DateTimeOffset ViewedAt { get; init; }
	public Account Account { get; init; } = null!;

	public static ViewedProduct Create(AccountId id, ProductId productId, DateTimeOffset viewedAt)
		=> new(id, productId, viewedAt);
}
