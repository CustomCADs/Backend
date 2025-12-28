using CustomCADs.Modules.Accounts.Application.Roles.Caching;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddRoleCaching()
			=> services.AddScoped<BaseCachingService<RoleId, Role>, RoleCachingService>();
	}
}
