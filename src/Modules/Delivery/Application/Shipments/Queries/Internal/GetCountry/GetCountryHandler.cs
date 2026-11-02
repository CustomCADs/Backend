using CustomCADs.Modules.Delivery.Application.Contracts;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetCountry;

public class GetCountryHandler(IDeliveryService service) : IQueryHandler<GetCountryQuery, int>
{
	public async Task<int> Handle(GetCountryQuery req, CancellationToken ct = default)
		=> await service.GetCountryAsync(req.Country, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<GetCountryQuery>.Custom($"No Country {req.Country} exists in the Delivery service.");
};
