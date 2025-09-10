using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;

using static CustomCADs.Shared.Application.Constants;

public sealed class ReportCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<ReportCustomCommand>
{
	public async Task Handle(ReportCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.AcceptedCustom?.DesignerId != req.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Report();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string designerName = await sender.SendQueryAsync(new GetUsernameByIdQuery(req.DesignerId), ct).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			new NotificationRequestedEvent(
				Type: NotificationType.CustomReported,
				Description: string.Format(Notifications.Messages.CustomReported, designerName),
				Link: Notifications.Links.CustomReported,
				AuthorId: req.DesignerId,
				ReceiverId: custom.BuyerId
			)
		).ConfigureAwait(false);
	}
}

