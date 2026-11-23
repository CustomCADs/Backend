using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetSortings;
using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetSortings;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountSortingsHandler handler = new();
	private readonly GetAccountSortingsQuery request = new();

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		AccountSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Enum.GetValues<AccountSortingType>()).IsEquivalentTo(sortings);
	}
}
