using CustomCADs.Modules.Files.Application.Images.Caching;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddImageCaching()
			=> services.AddScoped<BaseCachingService<ImageId, Image>, ImageCachingService>();
	}
}
