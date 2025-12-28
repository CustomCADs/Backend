using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.AddTag;


public sealed class AddProductTagHandler(
	IProductReads reads,
	IProductWrites writes,
	IUnitOfWork uow,
	IEventRaiser raiser
) : ICommandHandler<AddProductTagCommand>
{
	public async Task Handle(AddProductTagCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		await writes.AddTagAsync(req.Id, req.TagId, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductTagAdded,
				Description: ApplicationConstants.Notifications.Messages.ProductTagAdded,
				Link: ApplicationConstants.Notifications.Links.ProductTagAdded,
				AuthorId: req.CallerId,
				ReceiverIds: [product.CreatorId]
			)
		).ConfigureAwait(false);
	}
}
