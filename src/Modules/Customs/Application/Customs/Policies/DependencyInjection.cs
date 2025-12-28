using CustomCADs.Modules.Customs.Application.Customs.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddCustomsAccessPolicies()
			=> services
				.AddScoped<IFileDownloadPolicy<CadId>, CustomCadDownloadPolicy>()
				.AddScoped<IFileUploadPolicy<CadId>, CustomCadUploadPolicy>();
	}
}
