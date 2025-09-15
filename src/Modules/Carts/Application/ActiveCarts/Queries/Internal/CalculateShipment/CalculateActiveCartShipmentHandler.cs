using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

public sealed class CalculateActiveCartShipmentHandler(IActiveCartReads reads, IRequestSender sender)
	: IQueryHandler<CalculateActiveCartShipmentQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateActiveCartShipmentQuery req, CancellationToken ct)
	{
		ActiveCartItem[] items = await reads.AllAsync(req.CallerId, track: false, ct: ct).ConfigureAwait(false);

		if (!items.Any(x => x.ForDelivery))
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);
		}

		Dictionary<CustomizationId, double> weights = await sender.SendQueryAsync(
			query: new GetCustomizationsWeightByIdsQuery(
				Ids: [.. items
					.Where(x => x.ForDelivery && x.CustomizationId is not null)
					.Select(x => x.CustomizationId!.Value)
				]
			),
			ct: ct
		).ConfigureAwait(false);

		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateShipmentQuery(
				Weights: [.. weights
					.Select(x =>
					{
						ActiveCartItem item = items.First(item => item.CustomizationId == x.Key);
						return item.Quantity * x.Value / 1000;
					})
				],
				Address: req.Address
			),
			ct: ct
		).ConfigureAwait(false);

		return calculations;
	}
}
