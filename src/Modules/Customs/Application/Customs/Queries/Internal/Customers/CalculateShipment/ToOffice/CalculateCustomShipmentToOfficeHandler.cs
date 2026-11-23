using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToOffice;

public sealed class CalculateCustomShipmentToOfficeHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<CalculateCustomShipmentToOfficeQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateCustomShipmentToOfficeQuery req, CancellationToken ct)
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
			query: new CalculateShipmentToOfficeQuery(
				TotalWeight: weight * req.Count,
				OfficeId: req.OfficeId
			),
			ct: ct
		).ConfigureAwait(false);

		return calculations;
	}
}
