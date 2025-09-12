using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;

using static CustomCADs.Shared.Application.Constants;

public class SetCustomDeliveryHandler(ICustomReads reads, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<SetCustomDeliveryCommand>
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
				new NotificationRequestedEvent(
					Type: NotificationType.CustomToggledDelivery,
					Description: string.Format(Notifications.Messages.CustomToggledDelivery, custom.ForDelivery ? "on" : "off"),
					Link: Notifications.Links.CustomToggledDelivery,
					AuthorId: custom.BuyerId,
					ReceiverIds: [custom.AcceptedCustom.DesignerId]
				)
			).ConfigureAwait(false);
		}
	}
}
