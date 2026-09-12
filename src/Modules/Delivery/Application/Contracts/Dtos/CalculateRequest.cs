namespace CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

public record CalculateRequest(
	double[] Weights,
	Destination Destination,
	int? OfficeId,
	(int CountryId, long SiteId)? Address
);
