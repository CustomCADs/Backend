using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;


public sealed class CancelCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<CancelCustomCommand>
{
	public async Task Handle(CancelCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (req.CallerId != custom.AcceptedCustom?.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Cancel();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomCanceled,
				Description: ApplicationConstants.Notifications.Messages.CustomCanceled,
				Link: ApplicationConstants.Notifications.Links.CustomCanceled,
				AuthorId: req.CallerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);
	}
}

