using CustomCADs.Modules.Carts.Application.PurchasedCarts.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddPurchasedCartsAccessPolicies()
			=> services.AddScoped<IFileDownloadPolicy<CadId>, PurchasedCartCadDownloadPolicy>();
	}
}
