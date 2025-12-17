using CustomCADs.Modules.Catalog.Application.Products.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddProductsAccessPolicies()
			=> services
				.AddScoped<IFileDownloadPolicy<CadId>, ProductCadDownloadPolicy>()
				.AddScoped<IFileUploadPolicy<CadId>, ProductCadUploadPolicy>()
				.AddScoped<IFileReplacePolicy<CadId>, ProductCadReplacePolicy>();
	}
}
