using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

public sealed class GetActiveCartItemsHandler(IActiveCartReads reads, IRequestSender sender)
	: IQueryHandler<GetActiveCartItemsQuery, ActiveCartItemDto[]>
{
	public async Task<ActiveCartItemDto[]> Handle(GetActiveCartItemsQuery req, CancellationToken ct)
	{
		ActiveCartItem[] items = await reads.AllAsync(req.CallerId, track: false, ct: ct).ConfigureAwait(false);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(req.CallerId),
			ct: ct
		).ConfigureAwait(false);

		return [.. items.Select(x => x.ToDto(buyer))];
	}
}
