namespace CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

public record CalculateRequest(
	double[] Weights,
	string Country,
	string City,
	string Street
);
