using CustomCADs.Files.Application.Cads.Dtos;

namespace CustomCADs.Files.API.Cads;

internal static class Mapper
{
	internal static CadResponse ToResponse(this CadDto cad)
		=> new(
			Id: cad.Id.Value,
			Key: cad.Key,
			ContentType: cad.ContentType,
			Volume: cad.Volume,
			CamCoordinates: cad.CamCoordinates,
			PanCoordinates: cad.PanCoordinates,
			OwnerName: cad.OwnerName
		);
}
