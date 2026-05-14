using CustomCADs.Modules.Identity.API.Users.Dtos;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Queries.Get.MyAccount;

public class MyAccountMapper : ResponseMapper<MyAccountResponse, GetUserByUsernameDto>
{
	public override MyAccountResponse FromEntity(GetUserByUsernameDto user)
		=> new(
			Id: user.Id.Value,
			Role: user.Role,
			Username: user.Username,
			FirstName: user.FirstName,
			LastName: user.LastName,
			Email: user.Email.Value,
			TrackViewedProducts: user.TrackViewedProducts,
			CreatedAt: user.CreatedAt,
			ViewedProducts: [.. user.ViewedProducts.Select(ToResponse)],
			Fingerprints: [.. user.Fingerprints.Select(ToResponse)]
		);

	private static ViewedProductResponse ToResponse(ViewedProductDto product)
		=> new(
			Id: product.Id.Value,
			ViewedAt: product.ViewedAt
		);

	private static FingerprintResponse ToResponse(FingerprintDto fingerprint)
		=> new(
			Id: fingerprint.Id.Value,
			Device: fingerprint.Device,
			Location: fingerprint.Location,
			DeleteAllowed: fingerprint.DeleteAllowed,
			IssuedAt: fingerprint.IssuedAt
		);
}
