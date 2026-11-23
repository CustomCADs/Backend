namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Queries.Get.Reported;

public sealed record GetReportedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
