using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Put.PresignedUrl.Image;

public sealed record GetProductPutPresignedUrlRequest(
	Guid Id,
	UploadFileRequest File
);
