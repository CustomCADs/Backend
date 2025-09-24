using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.API.Materials.Endpoints.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
	string MaterialName,
	UploadFileRequest Image
);
