using CustomCADs.Notifications.Application.Notifications.Queries.Internal.Count;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get.Stats;

public sealed class GetNotificationsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<CountNotificationsDto>
{
	public override void Configure()
	{
		Get("stats");
		Group<NotificationsGroup>();
		Description(d => d
			.WithSummary("Stats")
			.WithDescription("See your Notifications' stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		CountNotificationsDto response = await sender.SendQueryAsync(
			new CountNotificationsQuery(
				ReceiverId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
