namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.PresignedCadUrl;

public sealed record GetCustomGetPresignedCadUrlResponse(
	string PresignedUrl,
	string ContentType
);
