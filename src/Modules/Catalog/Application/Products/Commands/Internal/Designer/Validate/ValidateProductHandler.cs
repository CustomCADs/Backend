using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.Validate;

public sealed class ValidateProductHandler(
	IProductReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<ValidateProductCommand>
{
	public async Task Handle(ValidateProductCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.DesignerId is not null)
		{
			throw CustomAuthorizationException<Product>.Custom($"A Designer has already checked this Product: {req.Id}.");
		}

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.CallerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CallerId, "User");
		}

		product.Validate(req.CallerId);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string designerName = await sender.SendQueryAsync(new GetUsernameByIdQuery(req.CallerId), ct).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductValidated,
				Description: string.Format(ApplicationConstants.Notifications.Messages.ProductValidated, designerName),
				Link: ApplicationConstants.Notifications.Links.ProductValidated,
				AuthorId: req.CallerId,
				ReceiverIds: [product.CreatorId]
			)
		).ConfigureAwait(false);
	}
}
