using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;

public sealed class GetUserByUsernameHandler(IUserService service, IRequestSender sender)
	: IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
{
	public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct = default)
	{
		User user = await service.GetByAccountIdAsync(req.Id).ConfigureAwait(false);

		AccountInfoDto info = await sender.SendQueryAsync(
			query: new GetAccountInfoByUsernameQuery(user.Username),
			ct: ct
		).ConfigureAwait(false);

		ViewedProductDto[] viewedProducts = await sender.SendQueryAsync(
			query: new GetAccountViewedProductsByUsernameQuery(user.Username),
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
			ViewedProducts: viewedProducts,
			Fingerprints: [.. user.RefreshTokens
				.Select(x => ToFingerprintDto(x, x.Value == req.RefreshToken))
				.OrderByDescending(x => x.IssuedAt)
			]
		);
	}

	private static FingerprintDto ToFingerprintDto(RefreshToken refreshToken, bool isCurrent)
		=> new(
			Id: refreshToken.Id,
			Device: refreshToken.Fingerprint.Device,
			Location: refreshToken.Fingerprint.Location,
			IssuedAt: refreshToken.IssuedAt,
			DeleteAllowed: !isCurrent
		);
}
