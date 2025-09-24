using CustomCADs.Notifications.Application.Notifications.Queries.Internal.Count;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Get.Stats;

public sealed class GetNotificationsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<GetNotificationsStatsResponse, GetNotificationsStatsMapper>
{
	public override void Configure()
	{
		Get("stats");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("Stats")
			.WithDescription("See your Notifications' stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		CountNotificationsDto counts = await sender.SendQueryAsync(
			query: new CountNotificationsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(counts, Map.FromEntity).ConfigureAwait(false);
	}
}
