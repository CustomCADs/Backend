using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using static CustomCADs.Shared.Application.Constants;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Delete;

public sealed class DeleteCustomHandler(ICustomReads reads, IWrites<Custom> writes, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<DeleteCustomCommand>
{
	public async Task Handle(DeleteCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.BuyerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		writes.Remove(custom);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (custom is { CustomStatus: not CustomStatus.Pending, AcceptedCustom: not null })
		{
			await raiser.RaiseApplicationEventAsync(
				new NotificationRequestedEvent(
					Type: NotificationType.CustomDeleted,
					Description: string.Format(Notifications.Messages.CustomDeleted, custom.ForDelivery ? "on" : "off"),
					Link: Notifications.Links.CustomDeleted,
					AuthorId: custom.BuyerId,
					ReceiverIds: [custom.AcceptedCustom.DesignerId]
				)
			).ConfigureAwait(false);
		}
	}
}
