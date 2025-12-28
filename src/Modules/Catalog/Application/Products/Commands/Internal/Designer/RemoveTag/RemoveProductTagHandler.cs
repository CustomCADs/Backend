using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;


public sealed class RemoveProductTagHandler(
	IProductReads reads,
	IProductWrites writes,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<RemoveProductTagCommand>
{
	public async Task Handle(RemoveProductTagCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		await writes.RemoveTagAsync(req.Id, req.TagId).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductTagRemoved,
				Description: ApplicationConstants.Notifications.Messages.ProductTagRemoved,
				Link: ApplicationConstants.Notifications.Links.ProductTagRemoved,
				AuthorId: req.CallerId,
				ReceiverIds: [product.CreatorId]
			)
		).ConfigureAwait(false);
	}
}
