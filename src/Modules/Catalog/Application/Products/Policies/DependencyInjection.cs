using CustomCADs.Catalog.Application.Products.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static void AddProductsAccessPolicies(this IServiceCollection services)
	{
		services.AddScoped<IFileDownloadPolicy<CadId>, ProductCadDownloadPolicy>();
		services.AddScoped<IFileUploadPolicy<CadId>, ProductCadUploadPolicy>();
		services.AddScoped<IFileReplacePolicy<CadId>, ProductCadReplacePolicy>();
	}
}
