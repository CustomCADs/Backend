using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed class GetAllShipmentsHandler(
	IShipmentReads reads,
	IRequestSender sender,
	BaseCachingService<ShipmentId, Shipment> cache
) : IQueryHandler<GetAllShipmentsQuery, Result<GetAllShipmentsDto>>
{
	public async Task<Result<GetAllShipmentsDto>> Handle(GetAllShipmentsQuery req, CancellationToken ct)
	{
		Result<Shipment> result = await cache.GetOrCreateAsync(
			factory: async () => await reads.AllAsync(
				query: new(
					CustomerId: req.CallerId,
					Sorting: req.Sorting,
					Pagination: req.Pagination
				),
				track: false,
				ct: ct
			).ConfigureAwait(false)
		).ConfigureAwait(false);

		Dictionary<AccountId, string> buyers = await sender.SendQueryAsync(
			query: new GetUsernamesByIdsQuery([.. result.Items.Select(x => x.BuyerId)]),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto(buyers[x.BuyerId]));
	}
}
