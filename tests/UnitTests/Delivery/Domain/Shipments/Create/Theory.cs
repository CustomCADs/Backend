namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create;

public record Theory(
	string Service,
	string? Email,
	string? Phone,
	string Recipient,
	int Count,
	double Weight,
	string Country,
	string City,
	string Street
);
