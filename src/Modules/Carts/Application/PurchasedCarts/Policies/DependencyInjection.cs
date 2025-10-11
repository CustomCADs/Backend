using CustomCADs.Carts.Application.PurchasedCarts.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static void AddPurchasedCartsAccessPolicies(this IServiceCollection services)
	{
		services.AddScoped<IFileDownloadPolicy<CadId>, PurchasedCartCadDownloadPolicy>();
	}
}
