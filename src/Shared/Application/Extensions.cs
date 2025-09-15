namespace CustomCADs.Shared.Application;

public static class Extensions
{
	public static int GetCountOrZero<TStatus>(
		this Dictionary<TStatus, int> counts,
		TStatus status
	) where TStatus : Enum
		=> counts.TryGetValue(status, out int count) ? count : 0;
}
