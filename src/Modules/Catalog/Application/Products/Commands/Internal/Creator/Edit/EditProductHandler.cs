using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Edit;


public sealed class EditProductHandler(
	IProductReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<EditProductCommand>
{
	public async Task Handle(EditProductCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		if (!await sender.SendQueryAsync(new GetCategoryExistsByIdQuery(req.CategoryId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CategoryId, "Category");
		}

		product
			.SetName(req.Name)
			.SetDescription(req.Description)
			.SetPrice(req.Price)
			.SetCategoryId(req.CategoryId);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductEdited,
				Description: ApplicationConstants.Notifications.Messages.ProductEdited,
				Link: string.Format(ApplicationConstants.Notifications.Links.ProductEdited, product.Id),
				AuthorId: req.CallerId,
				ReceiverIds: await sender.SendQueryAsync(
					new GetAccountsWithProductInCartQuery(product.Id),
					ct: ct
				).ConfigureAwait(false)
			)
		).ConfigureAwait(false);
	}
}
