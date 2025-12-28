using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Identity;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

public sealed class ToggleViewedProductsTrackingHandler(
	IUserService service,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<ToggleViewedProductsTrackingCommand>
{
	public async Task Handle(ToggleViewedProductsTrackingCommand req, CancellationToken ct = default)
	{
		AccountId accountId = await service.GetAccountIdAsync(req.Username).ConfigureAwait(false);
		AccountInfoDto info = await sender.SendQueryAsync(
			query: new GetAccountInfoByUsernameQuery(req.Username),
			ct: ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserEditedApplicationEvent(
				Id: accountId,
				TrackViewedProducts: !info.TrackViewedProducts
			)
		).ConfigureAwait(false);
	}
}
