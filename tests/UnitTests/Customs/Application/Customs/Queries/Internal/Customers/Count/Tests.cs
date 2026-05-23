using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.Count;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.Count;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CountCustomsHandler handler;
	private readonly CountCustomsQuery request = new(ValidBuyerId);

	private readonly Mock<ICustomReads> reads = new();

	private static readonly Dictionary<CustomStatus, int> Expected = new()
	{
		[CustomStatus.Pending] = 1,
		[CustomStatus.Accepted] = 2,
		[CustomStatus.Begun] = 3,
		[CustomStatus.Finished] = 4,
		[CustomStatus.Completed] = 5,
		[CustomStatus.Reported] = 6,
	};

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
			.ReturnsAsync(Expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.CountAsync(ValidBuyerId, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CountCustomsDto counts = await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(Expected[CustomStatus.Pending], counts.Pending),
			() => Assert.Equal(Expected[CustomStatus.Accepted], counts.Accepted),
			() => Assert.Equal(Expected[CustomStatus.Begun], counts.Begun),
			() => Assert.Equal(Expected[CustomStatus.Finished], counts.Finished),
			() => Assert.Equal(Expected[CustomStatus.Completed], counts.Completed),
			() => Assert.Equal(Expected[CustomStatus.Reported], counts.Reported)
		);
	}
}
