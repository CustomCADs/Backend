using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Admin.Remove;

public sealed class RemoveProductHandler(
	IProductReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<RemoveProductCommand>
{
	public async Task Handle(RemoveProductCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.CallerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CallerId, "User");
		}

		product.Remove();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductRemoved,
				Description: string.Format(ApplicationConstants.Notifications.Messages.ProductRemoved, product.Id),
				Link: ApplicationConstants.Notifications.Links.ProductRemoved,
				AuthorId: req.CallerId,
				ReceiverIds: product.DesignerId.HasValue
					? [product.CreatorId, product.DesignerId.Value]
					: [product.CreatorId]
			)
		).ConfigureAwait(false);
	}
}
