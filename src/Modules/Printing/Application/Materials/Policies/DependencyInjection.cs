using CustomCADs.Printing.Application.Materials.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static void AddMaterialsAccessPolicies(this IServiceCollection services)
	{
		services.AddScoped<IFileUploadPolicy<ImageId>, MaterialTextureUploadPolicy>();
		services.AddScoped<IFileReplacePolicy<ImageId>, MaterialTextureReplacePolicy>();
	}
}
