using CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;
using CustomCADs.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;
using CustomCADs.Shared.Application.Events.Carts;
using Wolverine;

namespace CustomCADs.Carts.Infrastructure.Sagas;

public class CartDeliveryPaymentSaga : Saga
{
	[Wolverine.Persistence.Sagas.SagaIdentity]
	public Guid Id { get; private set; }

	public static CartDeliveryPaymentSaga Start(CartPaymentStartedApplicationEvent msg)
		=> new() { Id = msg.Id };

	public async Task Handle(ActiveCartDeliveryRequestedApplicationEvent msg, ActiveCartDeliveryRequestedApplicationEventHandler handler)
	{
		await handler.Handle(msg).ConfigureAwait(false);
	}

	public async Task Handle(CartPaymentCompletedApplicationEvent msg, CartPaymentCompletedApplicationEventHandler handler)
	{
		await handler.Handle(msg).ConfigureAwait(false);

		MarkCompleted();
	}
}
