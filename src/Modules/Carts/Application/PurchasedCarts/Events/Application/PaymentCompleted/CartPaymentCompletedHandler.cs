using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Carts;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

public class CartPaymentCompletedHandler(
	IPurchasedCartReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEmailService email
) : IEventHandler<CartPaymentCompletedApplicationEvent>
{
	public async Task HandleAsync(CartPaymentCompletedApplicationEvent @event)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(@event.CartId).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(@event.CartId);

		cart.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await uow.BulkDeleteItemsByBuyerIdAsync(@event.BuyerId).ConfigureAwait(false);

		string recipient = await sender.SendQueryAsync(
			query: new GetUserEmailByIdQuery(@event.BuyerId)
		).ConfigureAwait(false);

		string clientUrl = await sender.SendQueryAsync(
			query: new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(recipient, $"{clientUrl}/carts/{cart.Id}").ConfigureAwait(false);

		if (cart.HasDelivery)
		{
			await sender.SendCommandAsync(
				command: new ActivateShipmentCommand(cart.ShipmentId!.Value)
			).ConfigureAwait(false);
			await email.SendRewardGrantedEmailAsync(recipient, $"{clientUrl}/shipments/{cart.ShipmentId}").ConfigureAwait(false);
		}
	}
}
