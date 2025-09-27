namespace CustomCADs.Delivery.Application.Contracts.Dtos;

public record ShipRequestDto(
	string Country,
	string City,
	string Street,
	string? Phone,
	string? Email,
	string Name,
	string Service,
	string Package,
	string Contents,
	double TotalWeight
);
