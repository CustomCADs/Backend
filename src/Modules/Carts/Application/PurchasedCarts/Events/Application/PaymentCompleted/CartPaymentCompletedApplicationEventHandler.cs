using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Carts;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

public class CartPaymentCompletedApplicationEventHandler(
	IPurchasedCartReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEmailService email
)
{
	public async Task HandleAsync(CartPaymentCompletedApplicationEvent ae)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(ae.CartId).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(ae.CartId);

		cart.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await uow.BulkDeleteItemsByBuyerIdAsync(ae.BuyerId).ConfigureAwait(false);

		string to = await sender.SendQueryAsync(
			query: new GetUserEmailByIdQuery(ae.BuyerId)
		).ConfigureAwait(false);

		string url = await sender.SendQueryAsync(
			query: new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(to, $"{url}/carts/{cart.Id}").ConfigureAwait(false);

		if (cart is { HasDelivery: true, ShipmentId: not null })
		{
			ShipmentId shipmentId = cart.ShipmentId.Value;

			await sender.SendCommandAsync(
				new ActivateShipmentCommand(shipmentId)
			).ConfigureAwait(false);
			await email.SendRewardGrantedEmailAsync(to, $"{url}/shipments/{shipmentId}").ConfigureAwait(false);
		}
	}
}
