
using CustomCADs.Notifications.Application.Notifications.Queries.Internal;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get;

public class GetNotificationsEndpoint(IRequestSender sender)
	: Endpoint<GetNotificationsRequest, Result<GetNotificationsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<NotificationsGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Notifications with Sort and Pagination options")
		);
	}

	public override async Task HandleAsync(GetNotificationsRequest req, CancellationToken ct)
	{
		Result<GetAllNotificationsDto> result = await sender.SendQueryAsync(
			new GetAllNotificationsQuery(
				Pagination: new(req.Page, req.Limit),
				ReceiverId: User.GetAccountId(),
				Sorting: new(req.SortingType, req.SortingDirection)
			),
			ct
		).ConfigureAwait(false);

		Result<GetNotificationsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(x => x.ToResponse())]
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
