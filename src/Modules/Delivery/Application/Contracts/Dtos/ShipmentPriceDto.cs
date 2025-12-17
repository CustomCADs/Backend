namespace CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

public record ShipmentPriceDto(
	double Amount,
	double Vat,
	double Total,
	string Currency
);
