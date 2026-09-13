using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToAddress;

public sealed class CalculateCustomShipmentToAddressHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<CalculateCustomShipmentToAddressQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateCustomShipmentToAddressQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (!custom.ForDelivery)
		{
			throw new CustomException("The Custom is not marked for delivery");
		}

		double weight = await sender.SendQueryAsync(
			query: new GetCustomizationWeightByIdQuery(req.CustomizationId),
			ct: ct
		).ConfigureAwait(false);

		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateShipmentToAddressQuery(
				TotalWeight: weight * req.Count,
				Country: req.Country,
				Site: req.Site
			),
			ct: ct
		).ConfigureAwait(false);

		return calculations;
	}
}
