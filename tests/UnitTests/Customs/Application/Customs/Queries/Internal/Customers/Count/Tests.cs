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

	[Test]
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

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CountCustomsDto counts = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(counts.Pending).IsEqualTo(Expected[CustomStatus.Pending]);
			await Assert.That(counts.Accepted).IsEqualTo(Expected[CustomStatus.Accepted]);
			await Assert.That(counts.Begun).IsEqualTo(Expected[CustomStatus.Begun]);
			await Assert.That(counts.Finished).IsEqualTo(Expected[CustomStatus.Finished]);
			await Assert.That(counts.Completed).IsEqualTo(Expected[CustomStatus.Completed]);
			await Assert.That(counts.Reported).IsEqualTo(Expected[CustomStatus.Reported]);
		}
	}
}
