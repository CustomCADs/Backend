using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Begin;


public sealed class BeginCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<BeginCustomCommand>
{
	public async Task Handle(BeginCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (req.CallerId != custom.AcceptedCustom?.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Begin();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomBegun,
				Description: ApplicationConstants.Notifications.Messages.CustomBegun,
				Link: ApplicationConstants.Notifications.Links.CustomBegun,
				AuthorId: req.CallerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);
	}
}

