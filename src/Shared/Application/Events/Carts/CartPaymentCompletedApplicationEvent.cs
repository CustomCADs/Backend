namespace CustomCADs.Shared.Application.Events.Carts;

public record CartPaymentCompletedApplicationEvent(
	PurchasedCartId CartId,
	AccountId BuyerId
) : BaseApplicationEvent
{
	public Guid Id => CartId.Value; // raw Guid for Saga Identity to work
}
