using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

public class GetCustomSortingsHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly GetCustomSortingsHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomSortingsQuery query = new();

		// Act
		CustomSortingType[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<CustomSortingType>());
	}
}
