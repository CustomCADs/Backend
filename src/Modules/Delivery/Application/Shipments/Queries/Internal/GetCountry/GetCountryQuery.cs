namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetCountry;

public record GetCountryQuery(string Country) : IQuery<int>;
