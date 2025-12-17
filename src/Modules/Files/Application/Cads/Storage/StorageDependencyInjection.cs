using CustomCADs.Modules.Files.Application.Cads.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class StorageDependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddCadStorageService()
			=> services.AddScoped<ICadStorageService, CadStorageService>();
	}
}
