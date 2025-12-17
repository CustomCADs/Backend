using CustomCADs.Modules.Catalog.Application.Tags.Caching;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddTagCaching()
			=> services.AddScoped<BaseCachingService<TagId, Tag>, TagCachingService>();
	}
}
