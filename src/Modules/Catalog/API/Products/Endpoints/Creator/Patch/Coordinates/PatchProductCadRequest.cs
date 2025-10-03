using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch.Coordinates;

public sealed record PatchProductCadRequest(
	Guid Id,
	CoordinateType Type,
	CoordinatesDto Coordinates
);
