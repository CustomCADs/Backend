using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed class GetAllShipmentsHandler(IShipmentReads reads)
	: IQueryHandler<GetAllShipmentsQuery, Result<GetAllShipmentsDto>>
{
	public async Task<Result<GetAllShipmentsDto>> Handle(GetAllShipmentsQuery req, CancellationToken ct)
	{
		Result<Shipment> result = await reads.AllAsync(
			query: new(
				CustomerId: req.CallerId,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto());
	}
}
