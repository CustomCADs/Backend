namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application.Edited;

public class ValidData : ITheoryData<Theory>
{
	public static IEnumerable<Theory> GetTestData()
	{
		// All NULL
		yield return new(null, null, null);

		// Valid Username
		yield return new("valid-username", null, null);

		// Valid Names
		yield return new(null, new(null, null), null);
		yield return new(null, new("valid-first-name", null), null);
		yield return new(null, new(null, "valid-last-name"), null);
		yield return new(null, new("valid-first-name", "valid-last-name"), null);

		// Valid TrackViewProducts
		yield return new(null, null, true);
		yield return new(null, null, false);
	}
}
