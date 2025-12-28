using System.Reflection;

namespace CustomCADs.Modules.Idempotency.Persistence;

public class IdempotencyPersistenceReference
{
	public static Assembly Assembly => typeof(IdempotencyPersistenceReference).Assembly;
}
