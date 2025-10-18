using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;

public sealed class FinishCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<FinishCustomCommand>
{
	public async Task Handle(FinishCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (!await sender.SendQueryAsync(new CadExistsByIdQuery(req.CadId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Custom>.ById(req.CadId, "Cad");
		}

		if (custom.AcceptedCustom?.DesignerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Finish(req.CadId, req.Price);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomFinished,
				Description: ApplicationConstants.Notifications.Messages.CustomFinished,
				Link: ApplicationConstants.Notifications.Links.CustomFinished,
				AuthorId: req.CallerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);
	}
}
