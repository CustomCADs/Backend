using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Modules.Printing.Persistence;

using static PersistenceConstants;

public class PrintingContext(DbContextOptions<PrintingContext> opts) : DbContext(opts)
{
	public required DbSet<Customization> Customizations { get; set; }
	public required DbSet<Material> Materials { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Printing);
		builder.ApplyConfigurationsFromAssembly(PrintingPersistenceReference.Assembly);
	}
}
