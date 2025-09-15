using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;


public sealed class EditCustomHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<EditCustomCommand>
{
	public async Task Handle(EditCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom
			.SetName(req.Name)
			.SetDescription(req.Description);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (custom is { CustomStatus: not CustomStatus.Pending, AcceptedCustom: not null })
		{
			await raiser.RaiseApplicationEventAsync(
				@event: new NotificationRequestedEvent(
					Type: NotificationType.CustomEdited,
					Description: ApplicationConstants.Notifications.Messages.CustomEdited,
					Link: ApplicationConstants.Notifications.Links.CustomEdited,
					AuthorId: custom.BuyerId,
					ReceiverIds: [custom.AcceptedCustom.DesignerId]
				)
			).ConfigureAwait(false);
		}
	}
}
