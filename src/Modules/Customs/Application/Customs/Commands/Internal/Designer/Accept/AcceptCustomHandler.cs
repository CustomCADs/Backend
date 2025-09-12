using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;

using static Shared.Application.Constants;

public sealed class AcceptCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<AcceptCustomCommand>
{
	public async Task Handle(AcceptCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.DesignerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Custom>.ById(req.DesignerId, "User");
		}

		custom.Accept(req.DesignerId);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string designerName = await sender.SendQueryAsync(new GetUsernameByIdQuery(req.DesignerId), ct).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			new NotificationRequestedEvent(
				Type: NotificationType.CustomAccepted,
				Description: string.Format(Notifications.Messages.CustomAccepted, designerName),
				Link: Notifications.Links.CustomAccepted,
				AuthorId: req.DesignerId,
				ReceiverIds: [custom.BuyerId]
			)
		).ConfigureAwait(false);
	}
}

