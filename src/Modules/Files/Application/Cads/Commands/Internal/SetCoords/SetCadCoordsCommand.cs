using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Cads.Commands.Internal.SetCoords;

public sealed record SetCadCoordsCommand(
	CadId Id,
	CoordinatesDto? CamCoordinates,
	CoordinatesDto? PanCoordinates,
	AccountId CallerId
) : ICommand;
