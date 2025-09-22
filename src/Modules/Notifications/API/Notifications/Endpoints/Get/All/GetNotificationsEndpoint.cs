using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetAll;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Get.All;

public class GetNotificationsEndpoint(IRequestSender sender)
	: Endpoint<GetNotificationsRequest, Result<GetNotificationsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Notifications with Sort and Pagination options")
		);
	}

	public override async Task HandleAsync(GetNotificationsRequest req, CancellationToken ct)
	{
		Result<GetAllNotificationsDto> result = await sender.SendQueryAsync(
			query: new GetAllNotificationsQuery(
				Pagination: new(req.Page, req.Limit),
				CallerId: User.GetAccountId(),
				Status: req.Status,
				Sorting: new(req.SortingType, req.SortingDirection)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToResponse())
		).ConfigureAwait(false);
	}
}
