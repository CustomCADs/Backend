using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;


public sealed class SetProductStatusHandler(
	IProductReads reads,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<SetProductStatusCommand>
{
	public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
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

		product.SetDesignerId(req.CallerId);

		switch (req.Status)
		{
			case ProductStatus.Validated: product.SetValidatedStatus(); break;
			case ProductStatus.Reported: product.SetReportedStatus(); break;
			default: throw CustomValidationException<Product>.Status(req.Status, product.Status);
		}

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string designerName = await sender.SendQueryAsync(new GetUsernameByIdQuery(req.CallerId), ct).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			@event: CreateNotificationRequestedEvent(
				status: req.Status,
				designerName: designerName,
				designerId: req.CallerId,
				creatorId: product.CreatorId,
				statusException: CustomStatusException<Product>.Forbidden(product.Id, req.Status.ToString())
			)
		).ConfigureAwait(false);
	}

	private static NotificationRequestedEvent CreateNotificationRequestedEvent(
		ProductStatus status,
		string designerName,
		AccountId designerId,
		AccountId creatorId,
		CustomStatusException<Product> statusException
	) => new(
			Type: status switch
			{
				ProductStatus.Validated => NotificationType.ProductValidated,
				ProductStatus.Reported => NotificationType.ProductReported,
				_ => throw statusException,
			},
			Description: status switch
			{
				ProductStatus.Validated => string.Format(ApplicationConstants.Notifications.Messages.ProductValidated, designerName),
				ProductStatus.Reported => string.Format(ApplicationConstants.Notifications.Messages.ProductReported, designerName),
				_ => throw statusException,
			},
			Link: status switch
			{
				ProductStatus.Validated => ApplicationConstants.Notifications.Links.ProductValidated,
				ProductStatus.Reported => ApplicationConstants.Notifications.Links.ProductReported,
				_ => throw statusException,
			},
			AuthorId: designerId,
			ReceiverIds: [creatorId]
		);
}
