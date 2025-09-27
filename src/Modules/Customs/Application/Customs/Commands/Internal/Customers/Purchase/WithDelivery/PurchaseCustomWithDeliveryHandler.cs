using CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;


public sealed class PurchaseCustomWithDeliveryHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IPaymentService payment,
	IEventRaiser raiser
) : ICommandHandler<PurchaseCustomWithDeliveryCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseCustomWithDeliveryCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		PurchaseCustomExtensions.EnsureCustomCanBePurchased(
			isDeliveryWrong: !custom.ForDelivery, // here, delivery is wrong if the custom **isn't** for delivery
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

		GetCustomizationCostByIdQuery costQuery = new(req.CustomizationId);
		decimal cost = await sender.SendQueryAsync(costQuery, ct).ConfigureAwait(false);
		decimal total = req.Count * (custom.FinishedCustom.Price + cost);

		double weight = await sender.SendQueryAsync(
			query: new GetCustomizationWeightByIdQuery(req.CustomizationId),
			ct: ct
		).ConfigureAwait(false);

		if (!await sender.SendQueryAsync(new GetCustomizationExistsByIdQuery(req.CustomizationId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Custom>.ById(req.CustomizationId.Value, "Customization");
		}

		custom.Complete(customizationId: req.CustomizationId);
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

		await raiser.RaiseApplicationEventAsync(
			@event: new CustomDeliveryRequestedApplicationEvent(
				Id: req.Id,
				ShipmentService: req.ShipmentService,
				Weight: req.Count * weight / 1000,
				Count: req.Count,
				Address: req.Address,
				Contact: req.Contact
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

		return response;
	}
}
