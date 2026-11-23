using CustomCADs.Modules.Identity.API.Users.Dtos;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Queries.Get.MyAccount;

public record MyAccountResponse(
	Guid Id,
	string Role,
	string Username,
	string? FirstName,
	string? LastName,
	string Email,
	bool TrackViewedProducts,
	DateTimeOffset CreatedAt,
	ViewedProductResponse[] ViewedProducts,
	FingerprintResponse[] Fingerprints
);
