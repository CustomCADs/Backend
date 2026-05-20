using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetSortings;
using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetSortings;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountSortingsHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAccountSortingsQuery query = new();

		// Act
		AccountSortingType[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<AccountSortingType>());
	}
}
