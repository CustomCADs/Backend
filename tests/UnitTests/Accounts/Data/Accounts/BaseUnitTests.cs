using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data.Accounts;

using static Data.Accounts.TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Account CreateAccount(string? role = null, string username = ValidUsername, string email = ValidEmail1, DateTimeOffset? createdAt = null, string? firstName = ValidFirstName, string? lastName = ValidLastName, AccountId? id = null)
		=> Account.CreateWithId(id ?? ValidId, role ?? Roles.TestData.ValidName, username, email, createdAt ?? DateTimeOffset.UtcNow, firstName, lastName);
}
