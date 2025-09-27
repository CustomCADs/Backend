namespace CustomCADs.Identity.API.Identity.Get.MyAccount;

public record MyAccountResponse(
	Guid Id,
	string Role,
	string Username,
	string? FirstName,
	string? LastName,
	string Email,
	bool TrackViewedProducts,
	DateTimeOffset CreatedAt
);
