using CustomCADs.Customs.Application.Customs.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static void AddCustomsAccessPolicies(this IServiceCollection services)
	{
		services.AddScoped<IFileDownloadPolicy<CadId>, CustomCadDownloadPolicy>();
		services.AddScoped<IFileUploadPolicy<CadId>, CustomCadUploadPolicy>();
	}
}
