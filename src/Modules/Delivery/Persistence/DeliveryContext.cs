using CustomCADs.Modules.Delivery.Domain.Shipments;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Delivery.Persistence;

using static PersistenceConstants;

public class DeliveryContext(DbContextOptions<DeliveryContext> opts) : DbContext(opts)
{
	public required DbSet<Shipment> Shipments { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Delivery);
		builder.ApplyConfigurationsFromAssembly(DeliveryPersistenceReference.Assembly);
	}
}
