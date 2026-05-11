using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;

public sealed record GetUserByUsernameQuery(
	AccountId Id,
	string? RefreshToken
) : IQuery<GetUserByUsernameDto>;
