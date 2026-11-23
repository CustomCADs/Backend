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

	public bool IsDeliveryStarted { get; set; }
	public bool IsDeliveryHandled { get; set; }

	public static CustomDeliveryPaymentSaga Start(CustomPaymentStartedApplicationEvent msg)
		=> new() { Id = msg.Id };

	public async Task Handle(CustomDeliveryRequestedApplicationEvent msg, CustomDeliveryRequestedHandler handler)
	{
		await HandleCustomDeliveryAsync(() => handler.HandleAsync(msg)).ConfigureAwait(false);
	}

	public async Task<ScheduledMessage<CustomPaymentCompletedApplicationEvent>?> Handle(CustomPaymentCompletedApplicationEvent msg, CustomPaymentCompletedHandler handler)
	{
		if (IsDeliveryStarted && !IsDeliveryHandled) return msg.ScheduledAt(DateTimeOffset.UtcNow.AddSeconds(1));

		await handler.HandleAsync(msg).ConfigureAwait(false);

		MarkCompleted();
		return null;
	}

	private async Task HandleCustomDeliveryAsync(Func<Task> handler)
	{
		IsDeliveryStarted = true;
		try
		{
			await handler.Invoke().ConfigureAwait(false);
		}
		catch
		{
			MarkCompleted();
			throw;
		}

		IsDeliveryHandled = true;
	}
}
