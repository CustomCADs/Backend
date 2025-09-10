using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal;

public class GetAllNotificationsHandler(INotificationReads reads, IRequestSender sender)
	: IQueryHandler<GetAllNotificationsQuery, Result<GetAllNotificationsDto>>
{
	public async Task<Result<GetAllNotificationsDto>> Handle(GetAllNotificationsQuery req, CancellationToken ct = default)
	{
		Result<Notification> result = await reads.AllAsync(
			query: new(
				ReceiverId: req.ReceiverId,
				Pagination: req.Pagination,
				Sorting: req.Sorting
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		Dictionary<AccountId, string> usernames = await sender.SendQueryAsync(
			new GetUsernamesByIdsQuery([.. result.Items.Select(x => x.AuthorId)]),
			ct: ct
		).ConfigureAwait(false);

		return new(
			Count: result.Count,
			Items: [.. result.Items.Select(x => x.ToGetAllDto(
				author: usernames[x.AuthorId])
			)]
		);
	}
}
