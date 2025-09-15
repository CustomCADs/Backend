using CustomCADs.Shared.Domain.Bases.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class ConfigurationUtilities
{
	public static string GetApplicationConnectionString<TException>(this IConfiguration config, string name, TException ex) where TException : BaseException
		=> config.GetConnectionString(name) ?? throw ex;
}
