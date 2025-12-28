using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;
using CustomCADs.Shared.Application.Events.Carts;
using Wolverine;

#pragma warning disable IDE1006 // Async suffix
namespace CustomCADs.Modules.Carts.Infrastructure.Sagas;

public class CartDeliveryPaymentSaga : Saga
{
	[Wolverine.Persistence.Sagas.SagaIdentity]
	public Guid Id { get; private set; }

	public static CartDeliveryPaymentSaga Start(CartPaymentStartedApplicationEvent msg)
		=> new() { Id = msg.Id };

	public async Task Handle(ActiveCartDeliveryRequestedApplicationEvent msg, ActiveCartDeliveryRequestedApplicationEventHandler handler)
	{
		await handler.HandleAsync(msg).ConfigureAwait(false);
	}

	public async Task Handle(CartPaymentCompletedApplicationEvent msg, CartPaymentCompletedApplicationEventHandler handler)
	{
		await handler.HandleAsync(msg).ConfigureAwait(false);

		MarkCompleted();
	}
}
