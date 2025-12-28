using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;

public sealed class GetAllNotificationsHandler(INotificationReads reads, IRequestSender sender)
	: IQueryHandler<GetAllNotificationsQuery, Result<GetAllNotificationsDto>>
{
	public async Task<Result<GetAllNotificationsDto>> Handle(GetAllNotificationsQuery req, CancellationToken ct = default)
	{
		Result<Notification> result = await reads.AllAsync(
			query: new(
				Pagination: req.Pagination,
				ReceiverId: req.CallerId,
				Status: req.Status,
				Sorting: req.Sorting
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		Dictionary<AccountId, string> usernames = await sender.SendQueryAsync(
			query: new GetUsernamesByIdsQuery([.. result.Items.Select(x => x.AuthorId)]),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto(author: usernames[x.AuthorId]));
	}
}
