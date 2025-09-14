using Microsoft.Extensions.Configuration;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class ConfigurationUtilities
{
	public static string GetApplicationConnectionString(this IConfiguration config, string name)
		=> config.GetConnectionString(name)
				?? throw new KeyNotFoundException($"Could not find connection string: '{name}'.");
}
