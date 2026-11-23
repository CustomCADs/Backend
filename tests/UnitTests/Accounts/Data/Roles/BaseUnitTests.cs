using CustomCADs.Modules.Accounts.Domain.Roles;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data.Roles;

using static Data.Roles.TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Role CreateRole(
		string? name = null,
		string? description = null,
		RoleId? id = null
	) => Role.CreateWithId(
			id: id ?? ValidId,
			name: name ?? ValidName,
			description: description ?? ValidDescription
		);
}
