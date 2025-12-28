using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Carts.Persistence;

using static PersistenceConstants;

public class CartsContext(DbContextOptions<CartsContext> opts) : DbContext(opts)
{
	public required DbSet<ActiveCartItem> ActiveCartItems { get; set; }
	public required DbSet<PurchasedCart> PurchasedCarts { get; set; }
	public required DbSet<PurchasedCartItem> PurchasedCartItems { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Carts);
		builder.ApplyConfigurationsFromAssembly(CartsPersistenceReference.Assembly);
	}
}
