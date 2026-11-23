using CustomCADs.Modules.Catalog.Domain.Categories;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Data.Categories;

using static TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Category CreateCategory(string name = ValidName, string description = ValidDescription, CategoryId? id = null)
		=> Category.CreateWithId(id ?? ValidId, name, description);
}
