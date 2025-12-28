using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Patch;

public sealed record PatchCadRequest(
	Guid Id,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates
);
