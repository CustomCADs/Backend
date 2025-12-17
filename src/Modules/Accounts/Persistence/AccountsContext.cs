using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Modules.Accounts.Domain.Roles;
using CustomCADs.Modules.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Modules.Accounts.Persistence;

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
