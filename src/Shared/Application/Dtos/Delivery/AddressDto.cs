namespace CustomCADs.Shared.Application.Dtos.Delivery;

public record AddressDto(
	string Country,
	string Site,
	string AddressNote,
	Geoposition Coordinates
);
