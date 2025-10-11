using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Files.Application.Cads;

internal static class Mapper
{
	internal static CadDto ToDto(this Cad cad, string ownerName)
		=> new(
			Id: cad.Id,
			Key: cad.Key,
			ContentType: cad.ContentType,
			Volume: cad.Volume,
			CamCoordinates: cad.CamCoordinates.ToDto(),
			PanCoordinates: cad.PanCoordinates.ToDto(),
			OwnerId: cad.OwnerId,
			OwnerName: ownerName
		);

	internal static Coordinates ToValueObject(this CoordinatesDto coordinates)
		=> new(coordinates.X, coordinates.Y, coordinates.Z);

	internal static CoordinatesDto ToDto(this Coordinates coordinates)
		=> new(coordinates.X, coordinates.Y, coordinates.Z);
}
