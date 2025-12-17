using CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;

namespace CustomCADs.Modules.Identity.API.Identity.Get.MyAccount;

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
			CreatedAt: user.CreatedAt
		);
}
