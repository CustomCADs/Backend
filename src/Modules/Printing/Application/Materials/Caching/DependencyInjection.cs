#pragma warning disable IDE0130
using CustomCADs.Modules.Printing.Application.Materials.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddMaterialCaching()
			=> services.AddScoped<BaseCachingService<MaterialId, Material>, MaterialCachingService>();
	}
}
