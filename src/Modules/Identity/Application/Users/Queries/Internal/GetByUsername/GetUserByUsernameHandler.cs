using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

public sealed class GetUserByUsernameHandler(IUserService service, IRequestSender sender)
	: IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
{
	public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct = default)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

		AccountInfoDto info = await sender.SendQueryAsync(
			query: new GetAccountInfoByUsernameQuery(req.Username),
			ct: ct
		).ConfigureAwait(false);

		ProductId[] viewedProductIds = await sender.SendQueryAsync(
			query: new GetAccountViewedProductsByUsernameQuery(req.Username),
			ct: ct
		).ConfigureAwait(false);

		return new(
			Id: user.Id,
			Role: user.Role,
			Username: user.Username,
			Email: user.Email,
			TrackViewedProducts: info.TrackViewedProducts,
			CreatedAt: info.CreatedAt,
			FirstName: info.FirstName,
			LastName: info.LastName,
			ViewedProductIds: viewedProductIds
		);
	}
}
