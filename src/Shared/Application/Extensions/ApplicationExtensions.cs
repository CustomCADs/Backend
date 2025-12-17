namespace CustomCADs.Shared.Application.Extensions;

public static class ApplicationExtensions
{
	extension<TStatus>(Dictionary<TStatus, int> counts) where TStatus : Enum
	{
		public int GetCountOrZero(TStatus status)
			=> counts.TryGetValue(status, out int count) ? count : 0;
	}
}
