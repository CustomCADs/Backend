using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Identity;

namespace CustomCADs.UnitTests.Identity.Data.Users;

using static TestData;

public class BaseUnitTests
{
	protected readonly CancellationToken ct = default;

	protected static User CreateUser(
		string? role = null,
		string? username = null,
		string? email = null,
		AccountId? accountId = null,
		bool? isVerified = null,
		UserId? id = null,
		RefreshToken[]? refreshTokens = null
	) => User.Create(
			id ?? ValidId,
			role ?? ValidRole,
			username ?? MinValidUsername,
			new(email ?? ValidEmail, isVerified ?? true),
			accountId ?? ValidAccountId,
			refreshTokens ?? []
		);
}
