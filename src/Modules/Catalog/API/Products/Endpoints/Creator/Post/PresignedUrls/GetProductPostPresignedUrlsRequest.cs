using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Post.PresignedUrls;

public sealed record GetProductPostPresignedUrlsRequest(
	string ProductName,
	UploadFileRequest Image,
	UploadFileRequest Cad
);
