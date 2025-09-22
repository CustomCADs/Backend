namespace CustomCADs.Printing.API.Materials.Endpoints.Get.PresignedUrl;

public sealed record GetMaterialGetPresignedUrlResponse(
	string PresignedUrl,
	string ContentType
);
