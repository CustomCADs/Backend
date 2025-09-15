using CustomCADs.Shared.Domain.Bases.Exceptions;

namespace CustomCADs.Shared.Persistence.Exceptions;

public class DatabaseConnectionException(string message, Exception? inner) : BaseException(message, inner)
{
	public static DatabaseConnectionException Missing(string name, Exception? inner = null)
		=> new($"Missing value for connection string: {name} in configuration", inner);

	public static DatabaseConnectionException Custom(string message, Exception? inner = null)
		=> new(message, inner);
}
