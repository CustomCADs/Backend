using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Cads.Dtos;

public record CadDto(
	CadId Id,
	string Key,
	string ContentType,
	decimal Volume,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates,
	AccountId OwnerId,
	string OwnerName
);
