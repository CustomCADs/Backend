using CustomCADs.Modules.Catalog.Application.Categories.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddCategoryCaching()
			=> services.AddScoped<BaseCachingService<CategoryId, Category>, CategoryCachingService>();
	}
}
