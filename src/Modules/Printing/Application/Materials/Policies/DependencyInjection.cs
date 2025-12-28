using CustomCADs.Modules.Printing.Application.Materials.Policies;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddMaterialsAccessPolicies()
			=> services
				.AddScoped<IFileUploadPolicy<ImageId>, MaterialTextureUploadPolicy>()
				.AddScoped<IFileReplacePolicy<ImageId>, MaterialTextureReplacePolicy>();
	}
}
