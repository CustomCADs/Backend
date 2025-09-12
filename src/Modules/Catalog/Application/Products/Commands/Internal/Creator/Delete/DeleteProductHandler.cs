using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Files;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Delete;

using static CustomCADs.Shared.Application.Constants;

public sealed class DeleteProductHandler(IProductReads reads, IProductWrites writes, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<DeleteProductCommand>
{
	public async Task Handle(DeleteProductCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CreatorId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		writes.Remove(product);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new ProductDeletedApplicationEvent(
				Id: product.Id,
				ImageId: product.ImageId,
				CadId: product.CadId
			)
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new NotificationRequestedEvent(
				Type: NotificationType.ProductDeleted,
				Description: Notifications.Messages.ProductDeleted,
				Link: Notifications.Links.ProductDeleted,
				AuthorId: req.CreatorId,
				ReceiverIds: await sender.SendQueryAsync(
					new GetAccountsWithProductInCartQuery(product.Id),
					ct
				).ConfigureAwait(false)
			)
		).ConfigureAwait(false);
	}
}
