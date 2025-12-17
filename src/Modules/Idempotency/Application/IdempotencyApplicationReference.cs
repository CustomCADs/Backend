using System.Reflection;

namespace CustomCADs.Modules.Idempotency.Application;

public class IdempotencyApplicationReference
{
	public static Assembly Assembly => typeof(IdempotencyApplicationReference).Assembly;
}
