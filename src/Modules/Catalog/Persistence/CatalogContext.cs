using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Persistence.ShadowEntities;
using CustomCADs.Shared.Persistence;

namespace CustomCADs.Catalog.Persistence;

using static PersistenceConstants;

public class CatalogContext(DbContextOptions<CatalogContext> opts) : DbContext(opts)
{
	public required DbSet<Product> Products { get; set; }
	public required DbSet<Category> Categories { get; set; }
	public required DbSet<Tag> Tags { get; set; }
	public required DbSet<ProductTag> ProductTags { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Catalog);
		builder.ApplyConfigurationsFromAssembly(CatalogPersistenceReference.Assembly);
	}
}
