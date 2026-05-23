using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly GetCustomSortingsHandler handler = new();
	private readonly GetCustomSortingsQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CustomSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<CustomSortingType>());
	}
}
