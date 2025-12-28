using CustomCADs.Modules.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentCompleted;
using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentStarted;
using CustomCADs.Shared.Application.Events.Customs;
using Wolverine;
using Wolverine.Persistence.Sagas;

#pragma warning disable IDE1006 // Async suffix
namespace CustomCADs.Modules.Customs.Infrastructure.Sagas;

public class CustomDeliveryPaymentSaga : Saga
{
	[SagaIdentity]
	public Guid Id { get; private set; }

	public static CustomDeliveryPaymentSaga Start(CustomPaymentStartedApplicationEvent msg)
		=> new() { Id = msg.Id };

	public async Task Handle(CustomDeliveryRequestedApplicationEvent msg, CustomDeliveryRequestedApplicationEventHandler handler)
	{
		await handler.HandleAsync(msg).ConfigureAwait(false);
	}

	public async Task Handle(CustomPaymentCompletedApplicationEvent msg, CustomPaymentCompletedApplicationEventHandler handler)
	{
		await handler.HandleAsync(msg).ConfigureAwait(false);

		MarkCompleted();
	}
}
