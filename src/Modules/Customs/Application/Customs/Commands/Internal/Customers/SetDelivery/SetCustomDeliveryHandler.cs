using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;


public sealed class SetCustomDeliveryHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<SetCustomDeliveryCommand>
{
	public async Task Handle(SetCustomDeliveryCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		custom.SetDelivery(req.Value);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (custom is { CustomStatus: not CustomStatus.Pending, AcceptedCustom: not null })
		{
			await raiser.RaiseApplicationEventAsync(
				@event: new NotificationRequestedEvent(
					Type: NotificationType.CustomToggledDelivery,
					Description: string.Format(ApplicationConstants.Notifications.Messages.CustomToggledDelivery, custom.ForDelivery ? "on" : "off"),
					Link: ApplicationConstants.Notifications.Links.CustomToggledDelivery,
					AuthorId: custom.BuyerId,
					ReceiverIds: [custom.AcceptedCustom.DesignerId]
				)
			).ConfigureAwait(false);
		}
	}
}
