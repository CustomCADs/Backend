using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Accounts.Persistence;

using static PersistenceConstants;

public class AccountsContext(DbContextOptions<AccountsContext> opt) : DbContext(opt)
{
	public required DbSet<Role> Roles { get; set; }
	public required DbSet<Account> Accounts { get; set; }
	public required DbSet<ViewedProduct> ViewedProducts { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Accounts);
		builder.ApplyConfigurationsFromAssembly(AccountPersistenceReference.Assembly);
	}
}
