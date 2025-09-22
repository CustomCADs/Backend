namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.PresignedUrls.Cad;

public sealed record GetProductGetPresignedUrlsResponse(
	string PresignedUrl,
	string ContentType
);
