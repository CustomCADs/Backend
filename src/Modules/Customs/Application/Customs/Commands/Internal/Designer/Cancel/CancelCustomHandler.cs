using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

using static ApplicationConstants;

public sealed class CancelCustomHandler(ICustomReads reads, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<CancelCustomCommand>
{
	public async Task Handle(CancelCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (req.DesignerId != custom.AcceptedCustom?.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Cancel();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new NotificationRequestedEvent(
				Type: NotificationType.CustomCanceled,
				Description: Notifications.Messages.CustomCanceled,
				Link: Notifications.Links.CustomCanceled,
				AuthorId: req.DesignerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);
	}
}

