using CustomCADs.Modules.Files.Application.Cads.Caching;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddCadCaching()
			=> services.AddScoped<BaseCachingService<CadId, Cad>, CadCachingService>();
	}
}
