using CustomCADs.Modules.Catalog.Domain.Tags;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Data.Tags;

using static TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Tag CreateTag(string? name = null, TagId? id = null)
		=> Tag.CreateWithId(id ?? ValidId, name ?? MinValidName);
}
