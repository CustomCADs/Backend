using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.API.Materials.Endpoints.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
	int Id,
	UploadFileRequest File
);
