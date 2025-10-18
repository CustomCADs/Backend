using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;

using static ApplicationConstants;

public sealed class CreateCustomHandler(
	IWrites<Custom> writes,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<CreateCustomCommand, CustomId>
{
	public async Task<CustomId> Handle(CreateCustomCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.CallerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Custom>.ById(req.CallerId, "User");
		}

		Custom custom = await writes.AddAsync(
			entity: Custom.Create(
				name: req.Name,
				description: req.Description,
				forDelivery: req.ForDelivery,
				buyerId: req.CallerId,
				category: req.CategoryId.HasValue
					? (req.CategoryId.Value, CustomCategorySetter.Customer)
					: null
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CustomCreated,
				Description: Notifications.Messages.CustomCreated,
				Link: Notifications.Links.CustomCreated,
				AuthorId: custom.BuyerId,
				ReceiverIds: [.. await sender.SendQueryAsync(
					query: new GetAccountIdsByRoleQuery(DomainConstants.Roles.Designer),
					ct: ct
				).ConfigureAwait(false)]
			)
		).ConfigureAwait(false);

		return custom.Id;
	}
}
