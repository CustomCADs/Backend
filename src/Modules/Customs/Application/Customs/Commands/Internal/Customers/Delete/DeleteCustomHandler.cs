using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Delete;

using static ApplicationConstants;

public sealed class DeleteCustomHandler(
	ICustomReads reads,
	IWrites<Custom> writes,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<DeleteCustomCommand>
{
	public async Task Handle(DeleteCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		writes.Remove(custom);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (custom is { CustomStatus: not CustomStatus.Pending, AcceptedCustom: not null })
		{
			await raiser.RaiseApplicationEventAsync(
				@event: new NotificationRequestedEvent(
					Type: NotificationType.CustomDeleted,
					Description: Notifications.Messages.CustomDeleted,
					Link: Notifications.Links.CustomDeleted,
					AuthorId: custom.BuyerId,
					ReceiverIds: [custom.AcceptedCustom.DesignerId]
				)
			).ConfigureAwait(false);
		}
	}
}
