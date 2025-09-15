namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountInfoByUsernameQuery(
	string Username
) : IQuery<AccountInfoDto>;

public sealed record AccountInfoDto(
	DateTimeOffset CreatedAt,
	bool TrackViewedProducts,
	string? FirstName,
	string? LastName
);
