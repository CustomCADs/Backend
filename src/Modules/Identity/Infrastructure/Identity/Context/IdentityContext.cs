using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Context;

public class IdentityContext(DbContextOptions<IdentityContext> opt) : IdentityDbContext<AppUser, AppRole, Guid>(opt)
{
	internal const string Schema = "Identity";

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.HasDefaultSchema(Schema);
		builder.ApplyConfigurationsFromAssembly(IdentityPersistenceReference.Assembly);
	}
}
