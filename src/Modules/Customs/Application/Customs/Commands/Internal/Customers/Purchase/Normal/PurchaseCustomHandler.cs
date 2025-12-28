using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentStarted;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

public sealed class PurchaseCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IPaymentService payment,
	IEventRaiser raiser
) : ICommandHandler<PurchaseCustomCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		PurchaseCustomExtensions.EnsureCustomCanBePurchased(
			isDeliveryWrong: custom.ForDelivery, // here, delivery is wrong if the custom **is** for delivery
			callerId: req.CallerId,
			ownerId: custom.BuyerId,
			acceptedCustom: custom.AcceptedCustom,
			finishedCustom: custom.FinishedCustom
		);

		string[] users = await Task.WhenAll(
			sender.SendQueryAsync(new GetUsernameByIdQuery(custom.BuyerId), ct),
			sender.SendQueryAsync(new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId), ct)
		).ConfigureAwait(false);

		string buyer = users[0], seller = users[1];
		decimal total = custom.FinishedCustom.Price;

		custom.Complete(customizationId: null);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomCompleted,
				Description: string.Format(ApplicationConstants.Notifications.Messages.CustomCompleted, custom.Name, seller),
				Link: ApplicationConstants.Notifications.Links.CustomCompleted,
				AuthorId: custom.AcceptedCustom.DesignerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);

		PaymentDto response = await payment.InitializeCustomPayment(
			paymentMethodId: req.PaymentMethodId,
			buyerId: req.CallerId,
			customId: custom.Id,
			total: total,
			description: (buyer, custom.Name, seller),
			ct: ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new CustomPaymentStartedApplicationEvent(req.Id.Value)
		).ConfigureAwait(false);

		return response;
	}
}
