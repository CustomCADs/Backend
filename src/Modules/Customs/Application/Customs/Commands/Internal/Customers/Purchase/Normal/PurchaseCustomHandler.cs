using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

using static ApplicationConstants;

public sealed class PurchaseCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment, IEventRaiser raiser)
	: ICommandHandler<PurchaseCustomCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.BuyerId)
		{
			throw CustomAuthorizationException<Custom>.ById(custom.Id);
		}

		if (custom.ForDelivery)
		{
			throw CustomException.Delivery<Custom>(custom.ForDelivery);
		}

		if (custom.AcceptedCustom is null)
		{
			throw CustomException.NullProp<Custom>(nameof(custom.AcceptedCustom));
		}

		if (custom.FinishedCustom is null)
		{
			throw CustomException.NullProp<Custom>(nameof(custom.FinishedCustom));
		}

		string[] users = await Task.WhenAll(
			sender.SendQueryAsync(new GetUsernameByIdQuery(custom.BuyerId), ct),
			sender.SendQueryAsync(new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId), ct)
		).ConfigureAwait(false);

		string buyer = users[0], seller = users[1];
		decimal total = custom.FinishedCustom.Price;

		custom.Complete(customizationId: null);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new NotificationRequestedEvent(
				Type: NotificationType.CustomCompleted,
				Description: string.Format(Notifications.Messages.CustomCompleted, custom.Name, seller),
				Link: Notifications.Links.CustomCompleted,
				AuthorId: custom.AcceptedCustom.DesignerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);

		PaymentDto response = await payment.InitializeCustomPayment(
			paymentMethodId: req.PaymentMethodId,
			buyerId: req.BuyerId,
			customId: custom.Id,
			price: total,
			description: $"{buyer} bought {custom.Name} from {seller}.",
			ct
		).ConfigureAwait(false);

		return response;
	}
}
