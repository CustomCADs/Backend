using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Customs;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Customs.Application.Customs.Events.Application.PaymentCompleted;

public class CustomPaymentCompletedApplicationEventHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEmailService email
)
{
	public async Task Handle(CustomPaymentCompletedApplicationEvent ae)
	{
		Custom custom = await reads.SingleByIdAsync(ae.CustomId).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(ae.CustomId);

		custom.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		string to = await sender.SendQueryAsync(
			query: new GetUserEmailByIdQuery(ae.BuyerId)
		).ConfigureAwait(false);

		string url = await sender.SendQueryAsync(
			query: new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(to, $"{url}/customs/{custom.Id}").ConfigureAwait(false);

		if (custom is { ForDelivery: true, CompletedCustom.ShipmentId: not null })
		{
			ShipmentId shipmentId = custom.CompletedCustom.ShipmentId.Value;

			await sender.SendCommandAsync(
				command: new ActivateShipmentCommand(shipmentId)
			).ConfigureAwait(false);
			await email.SendRewardGrantedEmailAsync(to, $"{url}/shipments/{shipmentId}").ConfigureAwait(false);
		}
	}
}
