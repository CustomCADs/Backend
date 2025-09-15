using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

public sealed class GetActiveCartItemHandler(IActiveCartReads reads, IRequestSender sender)
	: IQueryHandler<GetActiveCartItemQuery, ActiveCartItemDto>
{
	public async Task<ActiveCartItemDto> Handle(GetActiveCartItemQuery req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.CallerId, req.ProductId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.CallerId, req.ProductId });

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(req.CallerId),
			ct: ct
		).ConfigureAwait(false);

		return item.ToDto(buyer);
	}
}
