using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Modules.Files.Application.Cads;

internal static class Mapper
{
	extension(Cad cad)
	{
		internal CadDto ToDto(string ownerName)
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
	}

	extension(CoordinatesDto coordinates)
	{
		internal Coordinates ToValueObject()
			=> new(coordinates.X, coordinates.Y, coordinates.Z);
	}

	extension(Coordinates coordinates)
	{
		internal CoordinatesDto ToDto()
			=> new(coordinates.X, coordinates.Y, coordinates.Z);
	}

}
