using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Files.API.Cads.Dtos;

public record CadResponse(
	Guid Id,
	string Key,
	string ContentType,
	decimal Volume,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates,
	string OwnerName
);
