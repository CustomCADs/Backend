using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record GetCadCoordsByIdQuery(
	CadId Id
) : IQuery<GetCadCoordsByIdDto>;

public sealed record GetCadCoordsByIdDto(
	CoordinatesDto Cam,
	CoordinatesDto Pan
);
