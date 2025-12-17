using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Modules.Files.Domain.Images;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Files.Persistence;

using static PersistenceConstants;

public class FilesContext(DbContextOptions<FilesContext> opts) : DbContext(opts)
{
	public required DbSet<Cad> Cads { get; set; }
	public required DbSet<Image> Images { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Files);
		builder.ApplyConfigurationsFromAssembly(FilesPersistenceReference.Assembly);
	}
}
