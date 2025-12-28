using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.Report;

public sealed class ReportProductHandler(
	IProductReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<ReportProductCommand>
{
	public async Task Handle(ReportProductCommand req, CancellationToken ct)
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

		product.Report(req.CallerId);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string designerName = await sender.SendQueryAsync(new GetUsernameByIdQuery(req.CallerId), ct).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.ProductReported,
				Description: string.Format(ApplicationConstants.Notifications.Messages.ProductReported, designerName),
				Link: ApplicationConstants.Notifications.Links.ProductReported,
				AuthorId: req.CallerId,
				ReceiverIds: [product.CreatorId]
			)
		).ConfigureAwait(false);
	}
}
