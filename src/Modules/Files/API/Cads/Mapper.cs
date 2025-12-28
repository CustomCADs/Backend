using CustomCADs.Modules.Files.Application.Cads.Dtos;

namespace CustomCADs.Modules.Files.API.Cads;

internal static class Mapper
{
	extension(CadDto cad)
	{
		internal CadResponse ToResponse()
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

}
