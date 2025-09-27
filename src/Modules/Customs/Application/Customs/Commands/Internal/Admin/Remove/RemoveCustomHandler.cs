using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.Remove;

public class RemoveCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<RemoveCustomCommand>
{
	public async Task Handle(RemoveCustomCommand req, CancellationToken ct = default)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		custom.Remove();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomRemoved,
				Description: ApplicationConstants.Notifications.Messages.CustomRemoved,
				Link: ApplicationConstants.Notifications.Links.CustomRemoved,
				AuthorId: req.CallerId,
				ReceiverIds: GetReceiverIds(custom)
			)
		).ConfigureAwait(false);
	}

	private static AccountId[] GetReceiverIds(Custom custom)
		=> custom is { AcceptedCustom: not null }
			? [custom.BuyerId, custom.AcceptedCustom.DesignerId]
			: [custom.BuyerId];
}
