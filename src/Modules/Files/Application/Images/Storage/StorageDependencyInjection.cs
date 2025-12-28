using CustomCADs.Modules.Files.Application.Images.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class StorageDependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddImageStorageService()
			=> services.AddScoped<IImageStorageService, ImageStorageService>();
	}
}
