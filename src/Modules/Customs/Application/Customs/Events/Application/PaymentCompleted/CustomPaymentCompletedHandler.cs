using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Customs;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentCompleted;

public class CustomPaymentCompletedHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEmailService email
) : IEventHandler<CustomPaymentCompletedApplicationEvent>
{
	public async Task HandleAsync(CustomPaymentCompletedApplicationEvent @event)
	{
		Custom custom = await reads.SingleByIdAsync(@event.CustomId).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(@event.CustomId);

		custom.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		string recipient = await sender.SendQueryAsync(
			query: new GetUserEmailByIdQuery(@event.BuyerId)
		).ConfigureAwait(false);

		string clientUrl = await sender.SendQueryAsync(
			query: new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(recipient, $"{clientUrl}/customs/{custom.Id}").ConfigureAwait(false);

		if (custom.ForDelivery)
		{
			ShipmentId shipmentId = (custom.CompletedCustom?.ShipmentId)!.Value;

			await sender.SendCommandAsync(
				command: new ActivateShipmentCommand(shipmentId)
			).ConfigureAwait(false);

			await email.SendRewardGrantedEmailAsync(recipient, $"{clientUrl}/shipments/{shipmentId}").ConfigureAwait(false);
		}
	}
}
