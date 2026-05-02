using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;

public sealed record GetUserByUsernameDto(
	UserId Id,
	string Role,
	string Username,
	string? FirstName,
	string? LastName,
	bool TrackViewedProducts,
	Email Email,
	DateTimeOffset CreatedAt,
	ViewedProductDto[] ViewedProducts
);
