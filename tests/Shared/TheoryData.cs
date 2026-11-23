namespace CustomCADs.Tests.Shared;

public abstract class TheoryData<TData>;

public interface ITheoryData<TData>
{
	static IEnumerable<TData> GetTestData() => [];
}
